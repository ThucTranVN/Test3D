using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class HumanBone
{
    public HumanBodyBones bone;
    public float weight = 1.0f;
}

public class AIWeaponIK : MonoBehaviour
{
    public Transform targetTransform;
    public Transform aimTransform;
    public Vector3 targetOffset;
    public int iterations = 10;
    [Range(0,1)]
    public float weight = 1.0f;
    public HumanBone[] humanBones;
    private Transform[] bonesTransform;

    private float angleLimit;
    private float distanceLimit;
    private Animator animator;

    private void Awake()
    {
        if (DataManager.HasInstance)
        {
            angleLimit = DataManager.Instance.DataConfig.AngleLimit;
            distanceLimit = DataManager.Instance.DataConfig.DistanceLimit;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        bonesTransform = new Transform[humanBones.Length];
        for (int i = 0; i < bonesTransform.Length; i++)
        {
            bonesTransform[i] = animator.GetBoneTransform(humanBones[i].bone);
        }
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 targetDirection = (targetTransform.position + targetOffset) - aimTransform.position;
        Vector3 aimDirection = aimTransform.forward;
        float blendOut = 0f;
        float targetAngle = Vector3.Angle(targetDirection, aimDirection);

        if(targetAngle > angleLimit)
        {
            blendOut += (targetAngle - angleLimit) / 50f;
        }

        float targetDistance = targetDirection.magnitude;
        if(targetDistance < distanceLimit)
        {
            blendOut += distanceLimit - targetDistance;
        }

        Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, blendOut);
        return aimTransform.position + direction;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(aimTransform == null)
        {
            return;
        }

        if(targetTransform == null)
        {
            return;
        }

        Vector3 targetPosition = GetTargetPosition();
        //for (int j = 0; j < bonesTransform.Length; j++)
        //{
        //    Transform bone = bonesTransform[j];
        //    float boneWeight = humanBones[j].weight * weight;
        //    AimAtTarget(bone, targetPosition, boneWeight);
        //}
        for (int i = 0; i < iterations; i++)
        {
            for (int j = 0; j < bonesTransform.Length; j++)
            {
                Transform bone = bonesTransform[j];
                float boneWeight = humanBones[j].weight * weight;
                AimAtTarget(bone, targetPosition, boneWeight);
            }
        }
    }

    private void AimAtTarget(Transform bone, Vector3 targetPosition, float weight)
    {
        Vector3 aimDirection = aimTransform.forward;
        Vector3 targetDirection = targetPosition - aimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
        bone.rotation = blendedRotation * bone.rotation;
    }

    public void SetTargetTransform(Transform target)
    {
        targetTransform = target;
    }

    public void SetAimTransform(Transform aim)
    {
        aimTransform = aim;
    }
}
