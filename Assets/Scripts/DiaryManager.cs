using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryManager : MonoBehaviour
{
    public GameObject book;
    public GameObject bookOnTable;
    private bool questShow = false;

    public void activeBookOnTable()
    {
        if(book.active == false)
        {
            bookOnTable.active = true;
        }
    }
    public void setQuestShowTrue()
    {
        questShow = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && questShow == true)
        {
            book.active = false;
        }
    }
}
