using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float amountHealthPickup = 50f;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health)
        {
            health.TakeHealth(amountHealthPickup);
            Destroy(this.gameObject);
        }
    }
}
