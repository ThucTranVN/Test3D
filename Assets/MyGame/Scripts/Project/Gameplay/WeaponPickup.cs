using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public RaycastWeapon weaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
        //Player
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if(activeWeapon != null)
        {
            RaycastWeapon newWeapon = Instantiate(weaponPrefab);
            activeWeapon.Equip(newWeapon);
            Destroy(gameObject);
        }

        //AI
        AIWeapons aiWeapons = other.gameObject.GetComponent<AIWeapons>();
        if (aiWeapons != null)
        {
            RaycastWeapon newWeapon = Instantiate(weaponPrefab);
            aiWeapons.EquipWeapon(newWeapon);
            Destroy(gameObject);
        }
    }
}
