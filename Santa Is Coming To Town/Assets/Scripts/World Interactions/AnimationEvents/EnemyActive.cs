using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActive : MonoBehaviour
{
    [SerializeField] private GameObject santa;
    [SerializeField] private GameObject attackArea;
    [SerializeField] private GameObject introGrabArea;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject thisObj;
    [SerializeField] private ParticleSystem disappearFX;
    [SerializeField] private Animator anim;

    void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            //santa.SetActive(true);
            player.GetComponent<Movement>().enabled = false;
            cam.GetComponent<MouseLook>().mouseSensitivity = 0f;
            disappearFX.Play();
            anim.SetBool("startIntro", true);
            StartCoroutine(EnableMovement());
        }
    }

    IEnumerator EnableMovement()
    {
        thisObj.transform.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(4f);
        santa.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        santa.transform.GetComponent<AIMovement>().IntroMove();
        yield return new WaitForSeconds(15f);
        anim.SetBool("startIntro", false);
        attackArea.SetActive(true);
        introGrabArea.SetActive(false);
        player.GetComponent<Movement>().enabled = true;
        cam.GetComponent<MouseLook>().mouseSensitivity = 100f;
        yield return new WaitForSeconds(0.5f);
    }
}
