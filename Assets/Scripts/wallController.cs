using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wallController : MonoBehaviour
{
    GameObject gm;
    private void OnEnable()
    {
        if(GameManager.village_Count == 2)
        {
            GameObject gm = GameObject.Find("wrongWay");
            gm.SetActive(false);
        }
    }
}
