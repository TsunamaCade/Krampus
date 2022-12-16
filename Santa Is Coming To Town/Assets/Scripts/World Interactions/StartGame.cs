using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform player;
    [SerializeField] private Transform cam;
    private bool isPlaying = false;
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            anim.SetBool("hasStarted", true);
            isPlaying = true;
        }

        if(isPlaying == true)
        {
            if(!(anim.GetCurrentAnimatorStateInfo(0).IsName("WakeUp")))
            {
                StartCoroutine(EndStartingAnim());
            }
        }
    }

    IEnumerator EndStartingAnim()
    {
        yield return new WaitForSeconds(5.5f);
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<Movement>().enabled = true;
        cam.GetComponent<MouseLook>().enabled = true;
        anim.enabled = false;
    }
}
