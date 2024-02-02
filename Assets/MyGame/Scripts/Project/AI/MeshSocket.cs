using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSocket : MonoBehaviour
{
    public SocketID socketID;
    public HumanBodyBones bone;
    public Vector3 offSet;
    public Vector3 rotation;
    private Transform attatchPoint;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        attatchPoint = new GameObject("Socket_" + socketID).transform;
        attatchPoint.SetParent(animator.GetBoneTransform(bone));
        attatchPoint.localPosition = offSet;
        attatchPoint.localRotation = Quaternion.Euler(rotation);
    }

    public void Attach(Transform objectTransform)
    {
        objectTransform.SetParent(attatchPoint, false);
    }
}
