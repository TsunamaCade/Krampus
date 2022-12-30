using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamage : MonoBehaviour
{
    [SerializeField] public double health = 4;
    [SerializeField] private bool canBeDamaged = true;
    [SerializeField] private AIMovement move;
    [SerializeField] private GameObject santa;
    [SerializeField] private ParticleSystem damageFX;

    [SerializeField] private AudioSource disappearSound;

    [SerializeField] private YouWin yw;

    void OnTriggerEnter(Collider arrow)
    {
        if(arrow.CompareTag("Arrow") && canBeDamaged == true && health > 1f)
        {
            health -= 1;
            canBeDamaged = false;
            Instantiate(damageFX, santa.transform.position, Quaternion.identity);
            disappearSound.Play();
            move.Flee();
            StartCoroutine(DamageTime());
        }
        else if(arrow.CompareTag("Arrow") && canBeDamaged == true && health == 1f)
        {
            yw.EndGame();
        }

    }

    IEnumerator DamageTime()
    {
        yield return new WaitForSeconds(1f);
        canBeDamaged = true;
    }
}
