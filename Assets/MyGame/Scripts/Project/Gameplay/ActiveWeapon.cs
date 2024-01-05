using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1
    }

    public Transform[] weaponSlots;
    public Transform crossHairTarget;
    public Animator rigController;

    private RaycastWeapon[] equippedWeapons = new RaycastWeapon[2];
    private int activeWeaponIndex;

    void Start()
    {
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon)
        {
            Equip(existingWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var weapon = GetWeapon(activeWeaponIndex);
        if (weapon != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                weapon.StartFiring();
            }

            if (weapon.isFiring)
            {
                weapon.UpdateFiring(Time.deltaTime);
            }

            weapon.UpdateBullets(Time.deltaTime);

            if (Input.GetButtonUp("Fire1"))
            {
                weapon.StopFiring();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                bool isHolstered = rigController.GetBool("holster_weapon");
                rigController.SetBool("holster_weapon", !isHolstered);
            }
        }
    }

    private RaycastWeapon GetWeapon(int index)
    {
        if(index < 0 || index > equippedWeapons.Length)
        {
            return null;
        }

        return equippedWeapons[index];
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var weapon = GetWeapon(weaponSlotIndex);
        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        weapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);
        rigController.Play("equip_" + weapon.weaponName);

        equippedWeapons[weaponSlotIndex] = weapon;
        activeWeaponIndex = weaponSlotIndex;
    }
}
