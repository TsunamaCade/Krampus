using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveIntroAnimation : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private Transform hand;
    [SerializeField] private Transform player;
    [SerializeField] private Camera cam;
    [SerializeField] private AIMovement aiM;
    void Update()
    {
        if(aiM.introHasEntered == true)
        {
            player.position = new Vector3(hand.position.x + 1f, hand.position.y, hand.position.z);
            cam.transform.LookAt(head);
            if(aiM.introHasEntered == false)
            {
                this.enabled = false;
            }
        }
    }
}
