using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamage : MonoBehaviour
{
    [SerializeField] private double health = 4;
    [SerializeField] private bool canBeDamaged = true;
    [SerializeField] private AIMovement move;

    void OnTriggerEnter(Collider arrow)
    {
        if(arrow.CompareTag("Arrow") && canBeDamaged == true)
        {
            health -= 1;
            canBeDamaged = false;
            move.Flee();
            StartCoroutine(DamageTime());
        }
    }

    IEnumerator DamageTime()
    {
        yield return new WaitForSeconds(1f);
        canBeDamaged = true;
    }
}
