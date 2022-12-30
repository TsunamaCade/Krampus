using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool gameIsPaused;

    private AudioSource[] allAudioSources;

    void Start()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PausingGame();
        }
    }
    void PausingGame()
    {
        if(gameIsPaused)
        {
            foreach( AudioSource audioS in allAudioSources)
            {
                audioS.Pause();
            }
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else 
        {
            foreach( AudioSource audioS in allAudioSources)
            {
                audioS.UnPause();
            }
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
