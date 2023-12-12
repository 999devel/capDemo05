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
    bool isMonsterMove;

    private void Update()
    {
        if (isMonsterMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, 2.5f * Time.deltaTime);

            Vector3 targetDirection = nextPos.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f * Time.deltaTime);
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        StartCoroutine(Coroutine_AfterWatchingPlayer());
    }



    IEnumerator Coroutine_AfterWatchingPlayer()
    {
        yield return new WaitForSeconds(3.5f);
        //agent.destination = nextPos.position;
        isMonsterMove = true;
        anim.SetTrigger("isWalk");

        yield return new WaitForSeconds(6f);
        gameObject.SetActive(false);
    }
}
