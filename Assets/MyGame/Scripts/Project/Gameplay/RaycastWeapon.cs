using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public WeaponSlot weaponSlot;
    public string weaponName;
    public Transform raycastOrigin;
    public Transform raycastDestination;
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;
    public bool isFiring = false;
    public int fireRate = 25;
    public float bulletSpeed = 1000f;
    public float bulletDrop = 0f;

    private Ray ray;
    private RaycastHit hitInfo;
    private float accumulatedTime;
    private float maxLifeTime = 3f;
    public WeaponRecoil weaponRecoil;

    private void Awake()
    {
        weaponRecoil = GetComponent<WeaponRecoil>();
    }

    private Vector3 GetPosition(Bullet bullet)
    {
        //p + v*t + 0.5*g*t*t
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition) //p
            + (bullet.initialVelocity * bullet.time) //v*t 
            + (0.5f * bullet.time * bullet.time * gravity); // 0.5*g*t*t
    }

    public void StartFiring()
    {
        isFiring = true;
        accumulatedTime = 0f;
        FireBullet();
        weaponRecoil.Reset();
    }

    public void UpdateFiring(float deltaTime)
    {
        accumulatedTime += deltaTime;
        float fireInterval = 1.0f / fireRate;
        while(accumulatedTime >= 0f)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }

    public void UpdateBullets(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullets();
    }

    private void SimulateBullets(float deltaTime)
    {
        if (ObjectPool.HasInstance)
        {
            ObjectPool.Instance.pooledObjects.ForEach(bullet =>
            {
                Vector3 p0 = GetPosition(bullet);
                bullet.time += deltaTime;
                Vector3 p1 = GetPosition(bullet);
                RaycastSegment(p0, p1, bullet);
            });
        }
    }

    private void DestroyBullets()
    {
        if (ObjectPool.HasInstance)
        {
            foreach (Bullet bullet in ObjectPool.Instance.pooledObjects)
            {
                if (bullet.time >= maxLifeTime)
                {
                    bullet.Deactive();
                }
            }
        }
    }

    private void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;

        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            bullet.tracer.transform.position = hitInfo.point;
            bullet.time = maxLifeTime;
            end = hitInfo.point;

            var rigidbody = hitInfo.collider.GetComponent<Rigidbody>();
            if (rigidbody)
            {
                rigidbody.AddForceAtPosition(ray.direction * 2, hitInfo.point, ForceMode.Impulse);
            }
        }

        bullet.tracer.transform.position = end;
    }

    private void FireBullet()
    {
        PlayEffect();

        Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;

        if (ObjectPool.HasInstance)
        {
            var bullet = ObjectPool.Instance.GetPooledObject();
            bullet.Active(raycastOrigin.position, velocity);
        }

        weaponRecoil.GenerateRecoil(weaponName);
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
