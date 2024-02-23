using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Health Health;

    public void OnRayCastHit(RaycastWeapon weapon, Vector3 direction)
    {
        Health.TakeDamage(weapon.damageAmount, direction);
    }
}
