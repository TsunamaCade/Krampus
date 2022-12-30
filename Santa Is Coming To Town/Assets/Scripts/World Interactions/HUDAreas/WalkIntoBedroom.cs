using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkIntoBedroom : MonoBehaviour
{
    [SerializeField] private GameObject pickUpFlashlight;
    [SerializeField] private GameObject goToParentsRoom;

    void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            goToParentsRoom.SetActive(false);
            pickUpFlashlight.SetActive(true);
            StartCoroutine(Disable());
        }
    }

    void OnTriggerExit(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            pickUpFlashlight.SetActive(false);
            StopCoroutine(Disable());
        }
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(5f);
        pickUpFlashlight.SetActive(false);
    }
}
