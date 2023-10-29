using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private int QuestNum = 1;
    public bool IsQuestOneDone = false;
    public void inCreaseNum()
    {
        ++QuestNum;
        if(QuestNum == 4)
        {
            IsQuestOneDone = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            inCreaseNum();
        }
    }
}
