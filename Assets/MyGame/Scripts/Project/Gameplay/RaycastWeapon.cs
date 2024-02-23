using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public WeaponSlot weaponSlot;
    public string weaponName;
    public Transform raycastOrigin;
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
    public GameObject magazine;
    public int ammoCount;
    public int totalAmmo;
    public int magazineSize;
    public float damageAmount = 10f;
    public LayerMask layerMask;

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

    public bool CanReload()
    {
        return ammoCount == 0 && magazineSize > 0;
    }

    public bool EmptyAmmo()
    {
        return ammoCount == 0 && magazineSize <= 0;
    }

    public void RefillAmmo()
    {
        if(magazineSize > 0)
        {
            magazineSize--;
            ammoCount = totalAmmo;
        }
    }

    public void StartFiring()
    {
        isFiring = true;

        if(accumulatedTime > 0f)
        {
            accumulatedTime = 0f;
        }

        weaponRecoil.Reset();
    }

    public void UpdateWeapon(float deltaTime, Vector3 target)
    {
        if (isFiring)
        {
            UpdateFiring(deltaTime, target);           
        }

        UpdateBullets(deltaTime);
    }

    private void UpdateFiring(float deltaTime, Vector3 target)
    {
        accumulatedTime += deltaTime;
        float fireInterval = 1.0f / fireRate;
        while (accumulatedTime >= 0f)
        {
            FireBullet(target);
            accumulatedTime -= fireInterval;
        }
    }

    private void UpdateBullets(float deltaTime)
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

        if (Physics.Raycast(ray, out hitInfo, distance, layerMask))
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

            var hitBox = hitInfo.collider.GetComponent<HitBox>();
            if (hitBox)
            {
                hitBox.OnRayCastHit(this, ray.direction);
            }
        }

        bullet.tracer.transform.position = end;
    }

    private void FireBullet(Vector3 target)
    {
        if(ammoCount <= 0)
        {
            return;
        }

        ammoCount--;

        PlayEffect();

        Vector3 velocity = (target - raycastOrigin.position).normalized * bulletSpeed;

        if (ObjectPool.HasInstance)
        {
            var bullet = ObjectPool.Instance.GetPooledObject();
            bullet.Active(raycastOrigin.position, velocity);
        }

        if (weaponRecoil)
        {
            weaponRecoil.GenerateRecoil(weaponName);
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
