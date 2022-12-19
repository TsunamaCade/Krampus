using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBox : MonoBehaviour
{
    public bool opened = false;
    [SerializeField] private GameObject thisObj;

    [SerializeField] private GameOver go;

    void Update()
    {
        if(opened == true)
        {
            go.boxesLeft -= 1;
            thisObj.SetActive(false);
        }
    }

    void OnTriggerStay(Collider otherBox)
    {
        if(otherBox.CompareTag("Box"))
        {
            otherBox.GetComponent<FindOtherPlace>().SingleRelocate();
        }
    }
}
