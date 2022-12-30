using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Options : MonoBehaviour
{
    //Menu Variables
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject backButton;

    //Option Variables
    [SerializeField] private Toggle fullscreen;
    private bool isFullscreen;

    [SerializeField] private Dropdown resolution;

    [SerializeField] private Toggle vsync;

    [SerializeField] private Scrollbar mouseSensitivity;
    [SerializeField] private TMP_Text sensitivityPercentage;
    [SerializeField] private MouseLook ml;

    [SerializeField] private Scrollbar audioVolume;
    [SerializeField] private TMP_Text volumePercentage;
    [SerializeField] private AudioMixer mixer;

    //Misc Variables
    [SerializeField] private GameObject player;


    public void SwitchToOptionsMenu()
    {
        backButton.SetActive(true);
        startMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void SwitchToStartMenu()
    {
        backButton.SetActive(false);
        player.SetActive(true);
        startMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void ToggleFullscreen()
    {
        if(fullscreen.isOn)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            isFullscreen = true;
        }
        if(!fullscreen.isOn)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            isFullscreen = false;
        }
    }

    public void ChangeResolution()
    {
        if(resolution.value == 0)
        {
            Screen.SetResolution(1920, 1080, isFullscreen);
        }
        if(resolution.value == 1)
        {
            Screen.SetResolution(1280, 720, isFullscreen);
        }
        if(resolution.value == 2)
        {
            Screen.SetResolution(800, 600, isFullscreen);
        }
    }

    public void ToggleVsync()
    {
        if(vsync.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        if(!vsync.isOn)
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    public void ChangeSensitivity()
    {
        ml.mouseSensitivity = mouseSensitivity.value * 200;
        sensitivityPercentage.text = (Mathf.Round(mouseSensitivity.value * 100) + "%");
    }

    public void ChangeVolume()
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(audioVolume.value) * 50);
        volumePercentage.text = (Mathf.Round(audioVolume.value * 100) + "%");
        if(audioVolume.value <= 0f)
        {
            mixer.SetFloat("MasterVolume", -80f);
        }
    }
}
