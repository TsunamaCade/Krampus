using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    //Menu Variables
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject optionsMenu;

    //Option Variables
    [SerializeField] private Scrollbar mouseSensitivity;
    [SerializeField] private MouseLook ml;

    public void SwitchToOptionsMenu()
    {
        startMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void SwitchToStartMenu()
    {
        startMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void ChangeSensitivity()
    {
        ml.mouseSensitivity = mouseSensitivity.value * 100;
    }
}
