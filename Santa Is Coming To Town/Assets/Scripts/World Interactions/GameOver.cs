using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public int boxesLeft = 6;
    [SerializeField] private AIMovement aiM;

    void Update()
    {
        if(boxesLeft <= 0)
        {
            aiM.canSeePlayer = true;
        }
    }

    public void YouLose()
    {
        Time.timeScale = 0f;
    }
}
