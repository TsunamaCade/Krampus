using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindOtherPlace : MonoBehaviour
{
    [SerializeField] private Transform[] boxLocations;

    public void SingleRelocate()
    {
        this.transform.position = boxLocations[Random.Range(0, boxLocations.Length)].position;
    }
}
