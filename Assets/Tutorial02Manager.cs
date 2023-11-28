using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial02Manager : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerController playerController;

    public GameObject deadman_inthebox;
    public Transform forceDir;

    public void MovePlayerTuto01ToTuto02()
    {
        StartCoroutine(coTuto02ToVillage());
    }

    IEnumerator coTuto02ToVillage()
    {
        yield return new WaitForSeconds(1.5f);

        playerController.playerCanMove = false;

        gameManager.FadeIn(2.5f);
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene("Village");
    }

    public void ForceToDeadMan()
    {
        deadman_inthebox.GetComponent<Rigidbody>().AddForce(Vector3.right);
    }

}
