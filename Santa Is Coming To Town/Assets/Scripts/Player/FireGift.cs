using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGift : MonoBehaviour
{
    [SerializeField] private Transform arrowObj;
    [SerializeField] private Transform arrowSet;
    [SerializeField] private Rigidbody arrowRb;

    [SerializeField] private GameObject crossbow;
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            arrowRb.isKinematic = false;
            arrowRb.useGravity = true;
            arrowRb.AddForce(transform.up * 25f);
            StartCoroutine(DestroyGift());
        }
    }

    IEnumerator DestroyGift()
    {
        yield return new WaitForSeconds(1f);
        Destroy(crossbow);
    }
}
