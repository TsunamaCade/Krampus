using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;
    [SerializeField] private bool isOn = false;

    void Update()
    {
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
    }
}
