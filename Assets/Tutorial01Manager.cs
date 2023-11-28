using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


public class Tutorial01Manager : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerController playerController;

    public GameObject player_normal;
    public GameObject player_getBox;
    public GameObject trigger_SoldierMove;

    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public Transform soldierPos;
    public List<Transform> waypoint = new List<Transform>();

    bool isStartMove;
    int index;


    void FixedUpdate()
    {
        if (isStartMove)
        {
            navMeshAgent.destination = waypoint[index].localPosition;

            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 1f)
                GotoNext(); //목적지까지의 거리가 1이하거나 도착했으면 함수실행
            if (index == waypoint.Count)
                index = 0;
        }
    }

    void GotoNext()
    {
        navMeshAgent.destination = waypoint[index].position;
        index = (index + 1);
    }


    public void TriggerToMove()
    {
        isStartMove = true;
        animator.SetTrigger("isWalk");
    }

    //public void SceneToTuto02()
    //{
    //    StartCoroutine(LoadSceneToTuto02());
    //}


    //IEnumerator LoadSceneToTuto02()
    //{
    //    yield return new WaitForSeconds(2f);
    //    gameManager.FadeIn(4f);
    //    yield return new WaitForSeconds(5f);
    //    SceneManager.LoadScene("Tutorial02");
    //}


    public void MovePlayerTuto01ToTuto02()
    {
        StartCoroutine(Tuto01ToTuto02());
    }

    IEnumerator Tuto01ToTuto02()
    {
        playerController.playerCanMove = false;

        gameManager.FadeIn(2.5f);
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene("Tutorial02");
    }

    public void ChangePlayerToGetBox()
    {
        StartCoroutine(coChangePlayerToGetBox());
    }

    IEnumerator coChangePlayerToGetBox()
    {
        playerController.playerCanMove = false;
        gameManager.FadeIn(2f);
        yield return new WaitForSeconds(2f);
        player_normal.SetActive(false);
        player_getBox.SetActive(true);
        trigger_SoldierMove.SetActive(true);
        gameManager.FadeOut(2f);
        playerController.playerCanMove = true;
    }

}
