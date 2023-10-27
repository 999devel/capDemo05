using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject Option_Panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Room");
    }


    public void OpenOptionPanel()
    {
        Option_Panel.SetActive(true);
    }

    public void CloseOptionPanel()
    {
        Option_Panel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
