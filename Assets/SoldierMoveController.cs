using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierMoveController : MonoBehaviour
{
    [Header("Soldier Controller")]
    public NavMeshAgent soldierNav;
    public Animator soldierAnim;
    public List<Transform> SoldierWaypoint = new List<Transform>();
    int SoldierIndex;
    Coroutine coWaypoint;
    public GameObject Soldier_AtEntrance;


    [Header("Rotate to Player")]
    public Transform Player;
    bool isRotateToPlayer;
    float rotationSpeed = 2f;


    private void Update()
    {
        if (isRotateToPlayer)
        {
            Vector3 targetDirection = Player.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void StartRotation()
    {
        isRotateToPlayer = true;
    }

    public void StopRotation()
    {
        isRotateToPlayer = false;
    }




    public void SoliderGoForest()
    {
        coWaypoint = StartCoroutine(coSoliderGoForest());
    }


    IEnumerator coSoliderGoForest()
    {
        yield return new WaitForSeconds(0.7f);
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            soldierAnim.SetTrigger("isWalk");
            soldierNav.destination = SoldierWaypoint[SoldierIndex].position;

            if (!soldierNav.pathPending && soldierNav.remainingDistance < 1f)
                Soldier_GotoNext(); //목적지까지의 거리가 1이하거나 도착했으면 함수실행
            if (SoldierIndex == SoldierWaypoint.Count)
            {
                Soldier_AtEntrance.SetActive(false);
                StopCoroutine(coWaypoint);
            }
        }
    }

    void Soldier_GotoNext()
    {
        soldierNav.destination = SoldierWaypoint[SoldierIndex].position;
        SoldierIndex = (SoldierIndex + 1);
    }


}
