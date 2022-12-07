using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGift : MonoBehaviour
{
    [SerializeField] private Transform arrowObj;
    [SerializeField] private Transform arrowSet;
    [SerializeField] private Rigidbody arrowRb;
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            arrowRb.isKinematic = false;
            arrowRb.useGravity = true;
            arrowRb.AddForce(transform.up * 25f);
            StartCoroutine(ReplaceArrow());
        }
    }

    IEnumerator ReplaceArrow()
    {
        yield return new WaitForSeconds(1f);
        arrowRb.isKinematic = true;
        arrowRb.useGravity = false;
        arrowObj.transform.position = arrowSet.position;
    }
}
