using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public AIHealth AIHealth;

    public void OnRayCastHit(RaycastWeapon weapon, Vector3 direction)
    {
        AIHealth.TakeDamage(weapon.damageAmount, direction);
    }
}
