using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightController : MonoBehaviour
{
    private bool isInsideCollider = true;
    public List<LightContainer> lightContainers;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isInsideCollider)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                for (int i = 0; i < lightContainers.Count; i++)
                {
                    lightContainers[i].TurnOff();
                }
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                for (int i = 0; i < lightContainers.Count; i++)
                {
                    lightContainers[i].TurnOn();
                }
            }
        }
    }
}
