using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponRecoil : MonoBehaviour
{
    //[HideInInspector]
    public CharacterAiming characterAiming;
    public CinemachineImpulseSource cameraShake;
    public Animator rigController;
    public Vector2[] recoilPattern;
    public float duration;
    public float recoilModifier = 1.0f;

    private float time;
    private float verticalRecoil;
    private float horizontalRecoil;
    private int recoilIndex;

    private void Awake()
    {
        cameraShake = GetComponent<CinemachineImpulseSource>();
    }

    public void Reset()
    {
        recoilIndex = 0;
    }

    private int NextIndex(int index)
    {
        //int newIndex = index++;
        //if(newIndex == recoilPattern.Length)
        //{
        //    newIndex = 0;
        //}
        //return newIndex;
        return (index + 1) % recoilPattern.Length;
    }

    public void GenerateRecoil(string weaponName)
    {
        time = duration;

        cameraShake.GenerateImpulse(Camera.main.transform.forward);

        horizontalRecoil = recoilPattern[recoilIndex].x;
        verticalRecoil = recoilPattern[recoilIndex].y;

        recoilIndex = NextIndex(recoilIndex);
        rigController.Play("weapon_recoil_" + weaponName, 1, 0.0f);
    }

    private void Update()
    {
        if(time > 0)
        {
            characterAiming.yAxis.Value -= (((verticalRecoil/10) * Time.deltaTime) / duration) * recoilModifier;
            characterAiming.xAxis.Value -= (((horizontalRecoil/10) * Time.deltaTime) / duration) * recoilModifier;
            time -= Time.deltaTime;
        }
    }
}
