using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeapons : MonoBehaviour
{
    public float inAccurary = 0.4f; 
    private Animator animator;
    private RaycastWeapon currentWeapon;
    private MeshSocketController meshSocketController;
    private AIWeaponIK weaponIK;
    private Transform currentTarget;
    private bool activeWeapon = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        meshSocketController = GetComponent<MeshSocketController>();
        weaponIK = GetComponent<AIWeaponIK>();
    }

    private void Update()
    {
        if(currentTarget && currentWeapon && activeWeapon)
        {
            Vector3 target = currentTarget.position + weaponIK.targetOffset;
            target += Random.insideUnitSphere * inAccurary;
            currentWeapon.UpdateWeapon(Time.deltaTime, target);
        }
    }

    public void SetFiring(bool enable)
    {
        if (enable)
        {
            currentWeapon.StartFiring();
        }
        else
        {
            currentWeapon.StopFiring();
        }
    }

    public void EquipWeapon(RaycastWeapon weapon)
    {
        currentWeapon = weapon;
        meshSocketController.Attach(currentWeapon.transform, SocketID.Spine);
    }

    public void ActivateWeapon()
    {
        StartCoroutine(EquipWeapon());
    }

    private IEnumerator EquipWeapon()
    {
        animator.SetBool("Equip", true);
        yield return new WaitForSeconds(0.5f);
        while(animator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f)
        {
            yield return null;
        }
        weaponIK.SetAimTransform(currentWeapon.raycastOrigin);
        activeWeapon = true;
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

    public void SetTarget(Transform target)
    {
        weaponIK.SetTargetTransform(target);
        currentTarget = target;
    }
}
