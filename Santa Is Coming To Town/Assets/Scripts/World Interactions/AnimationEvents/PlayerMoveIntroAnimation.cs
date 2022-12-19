using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveIntroAnimation : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private Transform hand;
    [SerializeField] private Transform player;
    [SerializeField] private AIMovement aiM;

    private bool hasStarted = false;
    void Update()
    {
        if(aiM.introHasEntered == true)
        {
            player.position = new Vector3(hand.position.x + 1f, hand.position.y, hand.position.z);
            player.transform.LookAt(2 * player.transform.position - new Vector3(head.position.x, head.position.y - 0.5f, head.position.z));
            hasStarted = true;
        }

        if(aiM.introHasEntered == false && hasStarted == true)
        {
            player.position = new Vector3(player.position.x + 0.5f, player.position.y + 0.5f, player.position.z + 0.5f);
            this.enabled = false;
        }
    }
}
