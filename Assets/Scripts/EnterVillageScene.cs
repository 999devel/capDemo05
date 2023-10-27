using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterVillageScene : MonoBehaviour
{
    public GameObject enterVillageSceneButton;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            enterVillageSceneButton.SetActive(true);
        }
    }
}
