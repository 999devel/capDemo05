using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject enterVillageScenePanel;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void EnterVillageScene()
    {
        SceneManager.LoadScene("Village");
    }

    public void CloseEnterVillageSceneButtonPanel()
    {
        enterVillageScenePanel.SetActive(false);
    }
}
