using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    Rigidbody[] ragdollRigidbodies;

    void Start()
    {
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        StartCoroutine(UnActiveRagdoll());
    }

    IEnumerator UnActiveRagdoll()
    {
        yield return new WaitForSeconds(1.2f);

        foreach(Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
    }
}
