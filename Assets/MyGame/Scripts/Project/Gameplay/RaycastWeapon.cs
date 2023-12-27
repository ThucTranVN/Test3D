using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public Transform raycastOrigin;
    public Transform raycastDestination;
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;
    public bool isFiring = false;

    private Ray ray;
    private RaycastHit hitInfo;

    public void StartFiring()
    {
        isFiring = true;
        PlayEffect();

        ray.origin = raycastOrigin.position;
        ray.direction = raycastDestination.position - raycastOrigin.position;
        if(Physics.Raycast(ray, out hitInfo))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 10f);
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);
        }
    }

    public void StopFiring()
    {
        isFiring = false;
    }

    private void PlayEffect()
    {
        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }
    }
}
