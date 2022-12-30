using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private GameObject[] boxes;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject santa;
    [SerializeField] private GameObject santaAttackCollider;
    [SerializeField] private AIDamage aiD;
    [SerializeField] private GameObject gameOverScreen;
    
    public void Restart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gameOverScreen.SetActive(false);
        foreach(GameObject box in boxes)
        {
            box.transform.GetComponent<OpenBox>().opened = false;
            box.SetActive(true);
            box.GetComponent<FindOtherPlace>().SingleRelocate();
        }
        player.SetActive(true);
        StartCoroutine(ActivateSanta());
    }

    IEnumerator ActivateSanta()
    {
        yield return new WaitForSeconds(5f);
        santa.SetActive(true);
        santaAttackCollider.SetActive(true);
        santa.GetComponent<AIMovement>().Flee();
        santa.GetComponent<AIMovement>().Start();
        aiD.health = 5;
    }
}
