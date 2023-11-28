using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class EndingManager : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerController;
    public Transform playerGetUpRotatePos;

    public GameObject TriggerPlayerControllerActiver;

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

}
