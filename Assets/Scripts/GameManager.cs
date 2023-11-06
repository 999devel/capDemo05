using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject enterVillageScenePanel;
    public GameObject House_Collider_Door;
    public GameObject House_Collider2;
    public GameObject Village_PatrolGuideUIPanel;
    public GameObject[] Village_Walls;
    public GameObject[] Village_Doors;
    public GameObject entryWall;
    public static int village_Count = 0;

    public Transform playerPos;

    public Transform outOfHousePoint;
    public Transform IntoHousePoint;

    public Transform outOfForestPoint;
    public Transform IntoForestPoint;

    public void MovePlayerVillageToHouse()
    {
        playerPos.position = IntoHousePoint.position;
        playerPos.rotation = IntoHousePoint.rotation;
    }

    public void MovePlayerHouseToVillage()
    {
        playerPos.position = outOfHousePoint.position;
        playerPos.rotation = outOfHousePoint.rotation;
        //village_Count = 1;
    }
    public void MovePlayerVillageToForest()
    {
        playerPos.position = IntoForestPoint.position;
        playerPos.rotation = IntoForestPoint.rotation;
    }

    public void MovePlayerForestToVillage()
    {
        playerPos.position = outOfForestPoint.position;
        playerPos.rotation = outOfForestPoint.rotation;
    }


    //public void Day2EnterVillageScene()
    //{
    //    village_Count = 2;
    //}


    public void CloseEnterVillageSceneButtonPanel()
    {
        enterVillageScenePanel.SetActive(false);
    }

}
