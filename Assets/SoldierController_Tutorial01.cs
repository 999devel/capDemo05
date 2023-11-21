using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierController_Tutorial01 : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Animator animator;

    public Transform soldierPos;
    public List<Transform> waypoint = new List<Transform>();
    bool isStartMove;
    int index;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

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
}
