using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : Health
{
    private Ragdoll ragdoll;
    private ActiveWeapon activeWeapon;
    private CharacterAiming characterAiming;
    private VolumeProfile postProcessing;
    private Vignette vignette;

    protected override void OnStart()
    {
        ragdoll = GetComponent<Ragdoll>();
        activeWeapon = GetComponent<ActiveWeapon>();
        characterAiming = GetComponent<CharacterAiming>();
        postProcessing = FindObjectOfType<Volume>().profile;
    }

    protected override void OnDamage(Vector3 direction)
    {
        if(postProcessing.TryGet(out vignette))
        {
            float percent = 1.0f - (currentHealth / maxHealth);
            vignette.intensity.value = percent * 0.4f;
        }

    }

    protected override void OnDeath(Vector3 direction)
    {
        ragdoll.ActiveRagdoll();
        direction.y = 1f;
        ragdoll.ApplyFore(direction);
        activeWeapon.DropWeapon();
        characterAiming.enabled = false;
        if (CameraManager.HasInstance)
        {
            CameraManager.Instance.EnableKillCam();
        }
    }
}
