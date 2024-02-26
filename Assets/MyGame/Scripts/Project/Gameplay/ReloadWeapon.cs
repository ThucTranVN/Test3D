using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadWeapon : MonoBehaviour
{
    public Animator rigController;
    public WeaponAnimationEvent animationEvent;
    public ActiveWeapon activeWeapon;
    public Transform leftHand;
    public bool isReloading;

    private GameObject magazineHand;

    void Start()
    {
        animationEvent.WeaponAnimEvent.AddListener(OnAnimationEvent);
    }

    void Update()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        if(weapon != null)
        {
            if (weapon.EmptyAmmo()) return;

            if (Input.GetKeyDown(KeyCode.R) || weapon.CanReload())
            {
                isReloading = true;
                rigController.SetTrigger("reload_weapon");
            }
        }
    }

    private void OnAnimationEvent(string eventName)
    {
        Debug.Log($"Weapon Reload Event: {eventName}");

        switch (eventName)
        {
            case "detach_magazine":
                DetachMagazine();
                break;
            case "drop_magazine":
                DropMagazine();
                break;
            case "refill_magazine":
                RefillMagazine();
                break;
            case "attach_magazine":
                AttachMagazine();
                break;
        }
    }

    private void DetachMagazine()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_EJECTMAGAZINE);
        }
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        magazineHand = Instantiate(weapon.magazine, leftHand, true);
        weapon.magazine.SetActive(false);
    }

    private void DropMagazine()
    {
        GameObject droppedMagazine = Instantiate(magazineHand, magazineHand.transform.position, magazineHand.transform.rotation);
        droppedMagazine.transform.localScale = Vector3.one;
        droppedMagazine.AddComponent<Rigidbody>();
        droppedMagazine.AddComponent<BoxCollider>();
        magazineHand.SetActive(false);
    }

    private void RefillMagazine()
    {
        magazineHand.SetActive(true);
    }

    private void AttachMagazine()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_INSERTMAGAZINE);
        }
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        weapon.magazine.SetActive(true);
        Destroy(magazineHand);
        weapon.RefillAmmo();
        rigController.ResetTrigger("reload_weapon");
        isReloading = false;
    }
}
