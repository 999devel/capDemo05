using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseMonsterController : MonoBehaviour
{
    public Transform player;
    public GameManager gameManager;
    NavMeshAgent nav;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        nav.destination = player.position;

        if(!nav.pathPending && nav.remainingDistance < 1f)
        {
            gameManager.LoadDeathScene();
            gameObject.SetActive(false);
        }
    }
}
