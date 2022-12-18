using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimStart : MonoBehaviour
{
    [SerializeField] private AIMovement aiM;

    void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            Debug.Log("Entered");
            aiM.introHasEntered = true;
        }
    }
}
