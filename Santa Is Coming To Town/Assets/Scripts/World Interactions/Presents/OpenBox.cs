using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBox : MonoBehaviour
{
    public bool opened;

    void OnTriggerStay(Collider otherBox)
    {
        if(otherBox.CompareTag("Box"))
        {
            otherBox.GetComponent<FindOtherPlace>().SingleRelocate();
        }
    }
}
