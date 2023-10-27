using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject DialogueUI;
    public GameObject WorkUI;


    public void Interact()
    {
        if (gameObject.CompareTag("NPC"))
        {
            DialogueUI.SetActive(true);
        }

        if (gameObject.CompareTag("Well"))
        {
            WorkUI.SetActive(true);
        }
    }

}
