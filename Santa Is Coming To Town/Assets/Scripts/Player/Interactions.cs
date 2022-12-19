using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    //Gift Variables
    [SerializeField] private GameObject gift;
    [SerializeField] private Transform giftHoldLocation;
    [SerializeField] private Transform player;
    [SerializeField] private bool hasGift = false;

    //Flashlight Variables
    [SerializeField] private Flashlight fl;

    //Misc Variables
    [SerializeField] private GameObject grabImage;
    [SerializeField] private LayerMask mask;

    void Update()
    {
        RaycastHit hit;

        //Opening Present
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3f, mask))
        {
            grabImage.SetActive(true);

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
}
