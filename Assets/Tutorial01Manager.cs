using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Tutorial01Manager : MonoBehaviour
{
    public GameManager gameManager;


    public void SceneToTuto02()
    {
        StartCoroutine(LoadSceneToTuto02());
    }


    IEnumerator LoadSceneToTuto02()
    {
        yield return new WaitForSeconds(2f);
        gameManager.FadeIn(4f);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Tutorial02");
    }

}
