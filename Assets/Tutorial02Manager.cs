using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Tutorial02Manager : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerController playerController;
    public GameObject player;


    public void MovePlayerTuto01ToTuto02()
    {
        StartCoroutine(coTuto02ToVillage());
    }

    IEnumerator coTuto02ToVillage()
    {
        yield return new WaitForSeconds(1.5f);

        gameManager.BindPlayerMoving();

        gameManager.FadeIn(2.5f);
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene("Village");
    }

    public void WatchingDiedPeople()
    {
        StartCoroutine(coWatchingDiedPeople());
    }

    IEnumerator coWatchingDiedPeople()
    {
        gameManager.BindPlayerMoving();
        playerController.StartRotateToDiedPeople();
        yield return new WaitForSeconds(3f);
        gameManager.FadeIn(1f);
        yield return new WaitForSeconds(1f);
        playerController.StopRotateToDiedPeople();
        player.transform.DOLocalRotate(new Vector3(0, -90, 0), 0f);
        gameManager.FadeOut(1f);
        yield return new WaitForSeconds(1f);
        gameManager.UnBindPlayerMoving();
    }
}
