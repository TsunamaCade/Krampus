using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    //Flashlight Variables
    [SerializeField] private GameObject flashlight;
    [SerializeField] public bool isOn = false;

    //Gift Variables
    [SerializeField] private GameObject gift;
    [SerializeField] private Transform giftHoldLocation;
    [SerializeField] private Transform player;
    [SerializeField] private bool hasGift = false;

    void Update()
    {

        //Flashlight
        if(Input.GetButtonDown("Flashlight"))
        {
            isOn = !isOn;
        }

        if(isOn == true)
        {
            flashlight.SetActive(true);
        }
        else
        {
            flashlight.SetActive(false);
        }

        //Opening Present
        if(hasGift == false)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5f))
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
