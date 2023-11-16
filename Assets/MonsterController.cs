using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public GameManager gameManager;
    public Transform goal;
    NavMeshAgent agent;
    Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        StartCoroutine(Coroutine_AfterWatchingPlayer());
    }

    
    IEnumerator Coroutine_AfterWatchingPlayer()
    {
        yield return new WaitForSeconds(3f);
        agent.destination = goal.position;
        anim.SetTrigger("IsRun");
        gameManager.SFXSound_RunningMonster();

        yield return new WaitForSeconds(1.2f);
        gameObject.SetActive(false);
    }
}
