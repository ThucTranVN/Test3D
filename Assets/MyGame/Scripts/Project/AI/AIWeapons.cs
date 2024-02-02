using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeapons : MonoBehaviour
{
    private Animator animator;
    private RaycastWeapon currentWeapon;
    private MeshSocketController meshSocketController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        meshSocketController = GetComponent<MeshSocketController>();
    }

    public void EquipWeapon(RaycastWeapon weapon)
    {
        currentWeapon = weapon;
        meshSocketController.Attach(currentWeapon.transform, SocketID.Spine);
    }

    public void ActivateWeapon()
    {
        animator.SetBool("Equip", true);
    }

    public bool HasWeapon()
    {
        return currentWeapon != null;
    }

    public void OnAnimationEvent(string eventName)
    {
        if (eventName.Equals("equipWeapon"))
        {
            meshSocketController.Attach(currentWeapon.transform, SocketID.RightHand);
        }
    }

    public void DropWeapon()
    {
        if (HasWeapon())
        {
            currentWeapon.transform.SetParent(null);
            currentWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
            currentWeapon.gameObject.AddComponent<Rigidbody>();
            currentWeapon = null;
        }
    }
}
