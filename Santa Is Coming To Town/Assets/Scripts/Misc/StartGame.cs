using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject startScreenText;
    [SerializeField] private GameObject goToParentsRoom;
    [SerializeField] private GameObject lamp;
    [SerializeField] private Transform player;
    [SerializeField] private Transform cam;
    [SerializeField] private AudioSource turnOnLamp;
    private bool isPlaying = false;
    void Update()
    {
        if(isPlaying == true)
        {
            if(!(anim.GetCurrentAnimatorStateInfo(0).IsName("WakeUp")))
            {
                StartCoroutine(EndStartingAnim());
            }
        }
    }

    public void StartGameButton()
    {
        startScreenText.SetActive(false);
        StartCoroutine(StartAnimation());
    }

    IEnumerator EndStartingAnim()
    {
        yield return new WaitForSeconds(5.5f);
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<Movement>().enabled = true;
        cam.GetComponent<MouseLook>().enabled = true;
        cam.GetComponent<Interactions>().enabled = true;
        anim.enabled = false;
        yield return new WaitForSeconds(1f);
        goToParentsRoom.SetActive(true);
        yield return new WaitForSeconds(3f);
        goToParentsRoom.SetActive(false);
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        lamp.SetActive(true);
        turnOnLamp.Play();
        yield return new WaitForSeconds(1f);
        anim.SetBool("hasStarted", true);
        isPlaying = true;
    }
}
