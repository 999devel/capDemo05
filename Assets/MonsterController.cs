using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public GameManager gameManager;
    public Transform nextPos;
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
        yield return new WaitForSeconds(3.5f);
        agent.destination = nextPos.position;
        anim.SetTrigger("isWalk");

        yield return new WaitForSeconds(6f);
        gameObject.SetActive(false);
    }
}
