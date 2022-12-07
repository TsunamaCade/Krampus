using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject gift;
    [SerializeField] public bool isOn = false;
    [SerializeField] private bool getGift;

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
        if(getGift == false)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f))
            {
                if(hit.transform.CompareTag("Box"))
                {
                    if(hit.transform.GetComponent<OpenBox>().opened == false)
                    {
                        if(Input.GetButtonDown("Fire1"))
                        {
                            hit.transform.GetComponent<OpenBox>().opened = true;
                            getGift = true;
                        }
                    }
                    else if(hit.transform.GetComponent<OpenBox>().opened == true)
                    {
                        return;
                    }

                    
                }
            }
        }

        if(getGift == true)
        {
            gift.SetActive(true);
            StartCoroutine(HaveGift());
        }
    }

    IEnumerator HaveGift()
    {
        yield return new WaitForSeconds(0.5f);
        if(Input.GetButtonDown("Fire1"))
        {
            yield return new WaitForSeconds(1.001f);
            gift.SetActive(false);
            getGift = false;
        }
    }
}
