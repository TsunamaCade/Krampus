using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGift : MonoBehaviour
{
    [SerializeField] private Rigidbody arrowRb;
    [SerializeField] private CapsuleCollider arrowCol;

    [SerializeField] private GameObject crossbow;

    [SerializeField] private AudioSource arrow;

    void Start()
    {
        arrowCol.enabled = false;
        
    }
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            arrowRb.isKinematic = false;
            arrowRb.useGravity = true;
            arrowRb.AddForce(transform.up * 25f);
            arrowCol.enabled = true;
            arrow.Play();
            StartCoroutine(DestroyGift());
        }
    }

    IEnumerator DestroyGift()
    {
        yield return new WaitForSeconds(1f);
        Destroy(crossbow);
    }
}
