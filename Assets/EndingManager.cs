using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class EndingManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;
    public PlayerController playerController;
    public Transform playerGetUpRotatePos;

    public GameObject TriggerPlayerControllerActiver;
    public GameObject EndingUITrigger;

    public void PlayerGetUpAndETC()
    {
        StartCoroutine(coPlayerGetUpAndETC());
    }

    IEnumerator coPlayerGetUpAndETC()
    {
        player.transform.DOLocalRotate(new Vector3(0, -173.61f, 0), 3f);
        yield return new WaitForSeconds(3f);
        TriggerPlayerControllerActiver.SetActive(true);
    }
    public void ActionEnding()
    {
        StartCoroutine(coActionEnding());
    }

    IEnumerator coActionEnding()
    {
        playerController.playerCanMove = false;
        gameManager.playerTorch.SetActive(false);
        gameManager.FadeIn(2f);
        yield return new WaitForSeconds(2.1f);
        gameManager.FadeOut(2f);
        gameManager.FireGroup.SetActive(true);
        yield return new WaitForSeconds(2f);
        playerController.StartRotateToDiedPeople();
        yield return new WaitForSeconds(2f);
        playerController.StopRotateToDiedPeople();
        playerController.StartRotateToSky();
        yield return new WaitForSeconds(3f);
        EndingUITrigger.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");

    }

}
