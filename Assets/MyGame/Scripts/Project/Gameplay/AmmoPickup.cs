using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int magazineSize = 2;

    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon playerWeapon = other.GetComponent<ActiveWeapon>();
        if (playerWeapon)
        {
            playerWeapon.RefillMagazine(magazineSize);
            Destroy(this.gameObject);
        }
    }
}
