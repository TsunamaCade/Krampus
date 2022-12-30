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
    [SerializeField] private AudioSource boxDisappear;

    [SerializeField] private GameObject RunCrouch;

    void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isCrouched", false);
            player.GetComponent<Movement>().enabled = false;
            cam.GetComponent<MouseLook>().mouseSensitivity = 0f;
            disappearFX.Play();
            boxDisappear.Play();
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
        yield return new WaitForSeconds(14f);
        player.transform.rotation = new Quaternion(0, 180, 0, 0);
        anim.SetBool("startIntro", false);
        attackArea.SetActive(true);
        introGrabArea.SetActive(false);
        player.GetComponent<Movement>().enabled = true;
        cam.GetComponent<MouseLook>().mouseSensitivity = 100f;
        yield return new WaitForSeconds(0.5f);
        RunCrouch.SetActive(true);
        yield return new WaitForSeconds(3f);
        RunCrouch.SetActive(false);
    }
}
