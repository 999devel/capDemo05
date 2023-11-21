using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DirectionInteract : MonoBehaviour, IPointerClickHandler
{
    public Material material;

    private void Start()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        //material.color = new Color(0.5f, 0.5f, 0.7f);
        Debug.Log("onclick");
    }

    public void debug()
    {
        Debug.Log("onclick");
    }

    private void OnMouseDown()
    {
        Debug.Log("onclick");

    }


}
