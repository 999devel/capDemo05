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

    public void EnterHouse()
    {
        SceneManager.LoadScene("House");
        House_Collider_Door.SetActive(false);
        House_Collider2.SetActive(true);
    }

    public void EnterVillageScene()
    {
        SceneManager.LoadScene("Village");
        village_Count = 1;
    }

    public void Day2EnterVillageScene()
    {
        SceneManager.LoadScene("Village");
        village_Count = 2;
    }


    public void CloseEnterVillageSceneButtonPanel()
    {
        enterVillageScenePanel.SetActive(false);
    }

    public void EnterForest()
    {
        SceneManager.LoadScene("Forest");
    }
}
