using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScreamController : MonoBehaviour
{
    public GameManager gameManager;
    public Transform goal;
    NavMeshAgent agent;
    Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        StartCoroutine(Coroutine_TriggerRunning());
    }


    IEnumerator Coroutine_TriggerRunning()
    {
        agent.destination = goal.position;
        gameManager.SFXSound_Scream();

        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
