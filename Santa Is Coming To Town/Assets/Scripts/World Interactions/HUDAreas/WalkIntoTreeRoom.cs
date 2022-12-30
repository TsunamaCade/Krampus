using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkIntoTreeRoom : MonoBehaviour
{
    [SerializeField] private GameObject goBackToBed;

    void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            goBackToBed.SetActive(true);
            StartCoroutine(Disable());
        }
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(1f);
        goBackToBed.SetActive(false);
    }
}
