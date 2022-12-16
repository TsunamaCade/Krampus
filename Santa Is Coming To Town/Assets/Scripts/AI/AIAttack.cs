using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{
    [SerializeField] private GameOver gm;

    void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            gm.YouLose();
        }
    }
}
