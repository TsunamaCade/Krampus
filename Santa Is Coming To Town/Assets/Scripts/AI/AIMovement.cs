using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{

    [SerializeField] private Transform[] moveLocations;
    [SerializeField] private Vector3 moveTo;
    [SerializeField] private int Index;
    [SerializeField] private NavMeshAgent AI;
    [SerializeField] private float walkSpeed = 6f;

    [SerializeField] private float distanceLeft;

    [SerializeField] private bool canWander = true;
    [SerializeField] private bool hasWander = false;
    [SerializeField] private bool canChase = false;

    void Update()
    {
        distanceLeft = AI.remainingDistance;
        //Wander

        if(canWander == true && canChase == false)
        {
            StartCoroutine(ChangeLocation());
        }

        if(hasWander == true)
        {
            if(AI.remainingDistance <= 0)
            {
                StartCoroutine(MoveAgain());
            }
        }

        //Chase
        if(canChase == true)
        {
            Chase();
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
        hasWander = true;
    }

    IEnumerator MoveAgain()
    {
        hasWander = false;
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        canWander = true;
    }

    //Chase
    void Chase()
    {
        
    }
}
