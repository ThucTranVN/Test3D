using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    void Start()
    {
        SetupHitBox();
        OnStart();
    }

    private void SetupHitBox()
    {
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidBodies)
        {
            rigidbody.gameObject.AddComponent<HitBox>().Health = this;
            if (rigidbody.gameObject != gameObject)
            {
                rigidbody.gameObject.layer = LayerMask.NameToLayer("Hitbox");
            }
        }
    }

    public void TakeDamage(float damageAmount, Vector3 direction)
    {
        OnDamage(direction);
        currentHealth -= damageAmount;
        if (currentHealth <= 0f)
        {
            Die(direction);
        }
    }

    private void Die(Vector3 direction)
    {
        OnDeath(direction);
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnDeath(Vector3 direction)
    {

    }

    protected virtual void OnDamage(Vector3 direction)
    {

    }
}
