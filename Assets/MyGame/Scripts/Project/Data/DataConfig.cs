using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class DataConfig : ScriptableObject
{
    [Header("Player")]
    //Character locomotion
    public float JumpHeight;
    public float Gravity;
    public float StepDown;
    public float AirControl;
    public float JumpDamp;
    public float GroundSpeed;
    public float PushPower;
    //Character aiming
    public float TurnSpeed = 15f;
    //Health
    public float PlayerMaxHealth;

    [Header("AI")]
    //Health
    public float AIMaxHealth;
    public float BlinkDuration = 0.1f;
    //AiAgent
    public float MaxTime = 1f;
    public float MaxDistance = 5f;
    public float DieForce = 10f;
    public float MaxSightDistance = 10f;
    //AiWeapons
    public float InAccurary = 0.4f;
    //AiWeaponIK
    public float AngleLimit = 90f;
    public float DistanceLimit = 1.5f;

    [Header("UI")]
    public float NotifyLoadingTime = 5f;

    [Header("Game Component")]
    public float BounceSpeed = 8;
    public float BounceAmplitude = 0.05f;
    public float RotationSpeed = 90;
}
