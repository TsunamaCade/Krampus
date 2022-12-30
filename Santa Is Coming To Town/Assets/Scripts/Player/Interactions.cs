using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    [Header("Gift Variables")]
    [SerializeField] private GameObject gift;
    [SerializeField] private Transform giftHoldLocation;
    [SerializeField] private Transform player;
    [SerializeField] private bool hasGift = false;

    [SerializeField] private GameObject usePresent, HUD;

    [Header("Flashlight Variables")]
    [SerializeField] private Flashlight fl;

    [Header("Bed Variables")]
    [SerializeField] private GameObject secretEndScreen;

    [Header("Misc Variables")]
    [SerializeField] private GameObject grabImage;
    [SerializeField] private LayerMask mask;
    [SerializeField] private AudioSource aS;
    [SerializeField] private AudioClip openPresent;

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3f, mask))
        {
            grabImage.SetActive(true);

            //Opening Present
            if(hasGift == false)
            {
                if(hit.transform.CompareTag("Box"))
                {
                    if(hit.transform.GetComponent<OpenBox>().opened == false)
                    {
                        if(Input.GetButtonDown("Interact"))
                        {
                            hit.transform.GetComponent<OpenBox>().opened = true;
                            hasGift = true;
                            Instantiate(gift, player, false);
                            aS.PlayOneShot(openPresent, 1);
                            usePresent.SetActive(true);
                            StartCoroutine(Disable());
                        }
                    }
                    else if(hit.transform.GetComponent<OpenBox>().opened == true)
                    {
                        return;
                    }
                }
            }

            //Picking Up Flashlight
            if(hit.transform.CompareTag("Flashlight"))
            {
                if(Input.GetButtonDown("Interact"))
                {
                    fl.enabled = true;
                    hit.transform.gameObject.SetActive(false);
                }
            }

            //Going Back To Bed
            if(hit.transform.CompareTag("Bed"))
            {
                if(Input.GetButtonDown("Interact"))
                {
                    grabImage.SetActive(false);
                    player.gameObject.SetActive(false);
                    secretEndScreen.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
        else
        {
            grabImage.SetActive(false);
        }
        
        if(hasGift == true)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                hasGift = false;
            }
        }
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(4f);
        usePresent.SetActive(false);
        HUD.SetActive(false);
    }
}
