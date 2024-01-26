using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Rigidbody[] rigidBodies;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        DeactiveRagdoll();
    }

    public void DeactiveRagdoll()
    {
        foreach (var rigidBody in rigidBodies)  
        {
            rigidBody.isKinematic = true;
        }

        animator.enabled = true;
    }

    public void ActiveRagdoll()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = false;
        }

        animator.enabled = false;
    }
}
