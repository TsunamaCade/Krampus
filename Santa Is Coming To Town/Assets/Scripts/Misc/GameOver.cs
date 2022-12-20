using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public int boxesLeft = 6;
    [SerializeField] private AIMovement aiM;
    [SerializeField] private Transform playerSpawn;

    //Death Animation
    [SerializeField] private GameObject deathAnim;
    [SerializeField] private Transform santa;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gameOverScreen;

    void Update()
    {
        if(boxesLeft <= 0)
        {
            aiM.canSeePlayer = true;
        }
    }

    public void YouLose()
    {
        santa.gameObject.SetActive(false);
        player.transform.position = playerSpawn.position;
        player.SetActive(false);
        deathAnim.SetActive(true);
        deathAnim.transform.position = santa.position;
        deathAnim.transform.rotation = santa.rotation;
        StartCoroutine(EnableEndScreen());
    }

    IEnumerator EnableEndScreen()
    {
        yield return new WaitForSeconds(0.1f);
        deathAnim.transform.rotation = new Quaternion(deathAnim.transform.rotation.x, deathAnim.transform.rotation.y - 90f, deathAnim.transform.rotation.z, deathAnim.transform.rotation.w);
        yield return new WaitForSeconds(5.95f);
        deathAnim.SetActive(false);
        gameOverScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        
    }
}
