using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;
    [SerializeField] public bool isOn = false;
    
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
    }
}
