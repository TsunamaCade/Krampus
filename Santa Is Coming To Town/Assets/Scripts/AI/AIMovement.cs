using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private Transform[] moveLocations;
    [SerializeField] private Vector3 moveTo;
    [SerializeField] private NavMeshAgent AI;
    [SerializeField] private int Index;
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float runSpeed = 6f;

    [Header("State Variables")]
    [SerializeField] private float distanceLeft;

    [SerializeField] private bool canWander = true;
    [SerializeField] private bool wandering = false;
    [SerializeField] public bool canSeePlayer = false;
    [SerializeField] private bool hasChased = false;
    [SerializeField] private bool isFleeing = false;

    [Header("View Variables")]
    [SerializeField] public float radius;
    [Range(0,360)]
    [SerializeField] public float angle;

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstuctionMask;

    [Header("Animation Variables")]
    [SerializeField] private Animator anim;
    [SerializeField] private Transform santaAvatar;
    [SerializeField] private Transform runningPosition;
    [SerializeField] private Transform normalPosition;

    public bool introHasEntered = false;

    [Header("Misc Variables")]
    [SerializeField] private Transform player;
    [SerializeField] private Camera cam;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(ViewRepeat());
        santaAvatar.position = normalPosition.position;
        isFleeing = true;
    }

    void Update()
    {
        distanceLeft = AI.remainingDistance;

        //Wander
        if(isFleeing == false)
        {
            if(canWander == true && canSeePlayer == false)
            {
                StartCoroutine(ChangeLocation());
            }

            if(wandering == true)
            {
                if(distanceLeft <= 0 || AI.velocity.magnitude <=0)
                {
                    StartCoroutine(MoveAgain());
                }
            }

            //Chase
            if(canSeePlayer == true && hasChased == false)
            {
                hasChased = true;
                AI.ResetPath();
                santaAvatar.position = runningPosition.position;
                Chase();
            }
            if(canSeePlayer == false && hasChased == true)
            {
                hasChased = false;
                AI.ResetPath();
                santaAvatar.position = normalPosition.position;
                StartCoroutine(MoveAgain());
            }
        }

        if(introHasEntered == true)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("introStarted", true);
            StartCoroutine(EndIntroAnimation());
        }
    }


    //Wander
    void Move(float speed)
    {
        AI.speed = speed;
    }

    IEnumerator ChangeLocation()
    {
        anim.SetBool("isRunning", false);
        canWander = false;
        Index = Random.Range(0, moveLocations.Length);
        moveTo = moveLocations[Index].position;
        AI.SetDestination(moveTo);
        yield return new WaitForSeconds(1f);
        Move(walkSpeed);
        anim.SetBool("isWalking", true);
        wandering = true;
    }

    IEnumerator MoveAgain()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
        isFleeing = false;
        wandering = false;
        canWander = false;
        canSeePlayer = false;
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        canWander = true;
    }

    //Chase
    public void Chase()
    {
        AI.SetDestination(player.position);
        Move(runSpeed);
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", true);
    }

    public void IntroMove()
    {
        canSeePlayer = false;
        isFleeing = true;
        AI.SetDestination(player.position);
        AI.stoppingDistance = 3f;
        Move(walkSpeed);
        santaAvatar.position = normalPosition.position;
        anim.SetBool("isWalking", true);
        anim.SetBool("isRunning", false);
    }

    IEnumerator EndIntroAnimation()
    {
        AI.ResetPath();
        AI.isStopped = true;
        yield return new WaitForSeconds(4f);
        introHasEntered = false;
        moveTo = moveLocations[8].position;
        anim.SetBool("introStarted", false);

        AI.Warp(moveTo);
        canWander = false;
        canSeePlayer = false;
        isFleeing = false;
    }

    //Teleport Away
    public void Flee()
    {
        isFleeing = true;
        canWander = false;
        wandering = false;
        canSeePlayer = false;

        Index = Random.Range(0, moveLocations.Length);
        moveTo = moveLocations[Index].position;
        AI.Warp(moveTo);
        StartCoroutine(MoveAgain());

    }

    IEnumerator ViewRepeat()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return wait;
            ViewCheck();
        }
    }

    void ViewCheck()
    {
        if(cam.GetComponent<Flashlight>().isOn == true)
        {
            angle = 360f;
            radius = 20f;
        }
        else if(cam.GetComponent<Flashlight>().isOn == false)
        {
            angle = 100f;
            radius = 10f;
        }

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, playerMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstuctionMask))
                    canSeePlayer = true;
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
}