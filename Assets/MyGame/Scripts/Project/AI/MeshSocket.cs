using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSocket : MonoBehaviour
{
    public SocketID socketID;
    public Transform attatchPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Attach(Transform objectTransform)
    {
        objectTransform.SetParent(attatchPoint, false);
    }
}
