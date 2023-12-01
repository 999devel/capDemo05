using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTracking : MonoBehaviour
{
    public Transform playerPos;
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.LookAt(playerPos);
    }

    public void NPCLookAtPlayerOnStartConversation()
    {
        transform.LookAt(playerPos);
    }

}
