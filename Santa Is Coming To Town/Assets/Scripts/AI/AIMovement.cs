using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    //Movement Variables
    [SerializeField] private Transform[] moveLocations;
    [SerializeField] private Vector3 moveTo;
    [SerializeField] private NavMeshAgent AI;
    [SerializeField] private int Index;
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float runSpeed = 6f;

    //State Variables
    [SerializeField] private float distanceLeft;

    [SerializeField] private bool canWander = true;
    [SerializeField] private bool wandering = false;
    [SerializeField] public bool canSeePlayer = false;
    [SerializeField] private bool isFleeing = false;

    //View Variables
    [SerializeField] public float radius;
    [Range(0,360)]
    [SerializeField] public float angle;

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstuctionMask;

    //Misc Variables
    [SerializeField] public Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(ViewRepeat());
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
            if(canSeePlayer == true)
            {
                AI.ResetPath();
                Chase();
            }
        }
    }


    //Wander
    void Move(float speed)
    {
        AI.speed = speed;
    }

    IEnumerator ChangeLocation()
    {
        canWander = false;
        Index = Random.Range(0, moveLocations.Length);
        moveTo = moveLocations[Index].position;
        AI.SetDestination(moveTo);
        yield return new WaitForSeconds(1f);
        Move(walkSpeed);
        wandering = true;
    }

    IEnumerator MoveAgain()
    {
        isFleeing = false;
        wandering = false;
        canWander = false;
        canSeePlayer = false;
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        canWander = true;
    }

    //Chase
    void Chase()
    {
        AI.SetDestination(player.position);
        Move(runSpeed);
    }

    //Run Away
    public void Flee()
    {
        isFleeing = true;
        canWander = false;
        wandering = false;
        canSeePlayer = false;
        float furthestDist = 50f;

        foreach (Transform movetowards in moveLocations)
        {
            float dist = (movetowards.position - transform.position).sqrMagnitude;

            if(dist>furthestDist)
            {
                AI.SetDestination(movetowards.position);
                Move(runSpeed);
            }
        }

        if(distanceLeft <= 1)
        {
            Debug.Log("Met");
            StartCoroutine(MoveAgain());
        }
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
        if(player.GetComponent<Interactions>().isOn == true)
        {
            angle = 360f;
        }
        else if(player.GetComponent<Interactions>().isOn == false)
        {
            angle = 100f;
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