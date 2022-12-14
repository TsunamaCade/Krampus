using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLocationRandomizer : MonoBehaviour
{
    [SerializeField] private Transform[] boxes;
    [SerializeField] private Transform[] boxLocations;

    [SerializeField] private GameObject thisObj;

    void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            foreach(Transform box in boxes)
            {
                box.rotation = new Quaternion(0f, 0f, 0f, 0f);
                box.position = boxLocations[Random.Range(0, boxLocations.Length)].position;
            }
        }
    }
}
