using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWin : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawn;
    [SerializeField] private GameObject santa;
    [SerializeField] private GameObject santaDeath;
    [SerializeField] private GameObject youWinScreen;
    [SerializeField] private ParticleSystem damageFX;

    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            santaDeath.transform.position = santa.transform.position;
            santaDeath.transform.rotation = santa.transform.rotation;
            StartCoroutine(TimeToEnd());
        }
    }

    public void EndGame()
    {
        santaDeath.transform.position = santa.transform.position;
        StartCoroutine(TimeToEnd());
    }

    IEnumerator TimeToEnd()
    {
        santa.SetActive(false);
        santaDeath.SetActive(true);
        santaDeath.transform.rotation = santa.transform.rotation;
        yield return new WaitForSeconds(2.5f);
        damageFX.Play();
        yield return new WaitForSeconds(0.25f);
        santaDeath.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        player.transform.position = playerSpawn.position;
        player.SetActive(false);
        youWinScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
