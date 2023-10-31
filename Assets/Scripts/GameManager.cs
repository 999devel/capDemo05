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



    public void EnterHouse()
    {
        SceneManager.LoadScene("House");
        House_Collider_Door.SetActive(false);
        House_Collider2.SetActive(true);
    }

    public void EnterVillageScene()
    {
        SceneManager.LoadScene("Village");
    }

    public void Day2EnterVillageScene()
    {
        SceneManager.LoadScene("Village");
        Village_PatrolGuideUIPanel.SetActive(true);
        for (int i = 0; i < Village_Walls.Length; i++)
        {
            Village_Walls[i].SetActive(false);
        }
        for (int i = 0; i < Village_Doors.Length; i++)
        {
            Village_Doors[i].SetActive(true);
        }
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
