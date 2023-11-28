using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{


    public void ImmediatelyLoadTuto01Scene()
    {
        SceneManager.LoadScene("Tutorial01");
    }

    public void LoadTuto01Scene()
    {
        StartCoroutine(coLoadTuto01Scene());
    }

    IEnumerator coLoadTuto01Scene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Tutorial01");
    }
}
