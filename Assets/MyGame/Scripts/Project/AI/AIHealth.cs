using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    private Ragdoll ragdoll;

    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        currentHealth = maxHealth;
        SetupHixBox();
    }

    private void SetupHixBox()
    {
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidBodies)
        {
            rigidbody.gameObject.AddComponent<HitBox>().AIHealth = this;
        }
    }

    public void TakeDamage(float damageAmount, Vector3 direction)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        if (ragdoll)
        {
            ragdoll.ActiveRagdoll();
        }
    }
}
