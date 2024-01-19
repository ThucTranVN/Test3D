using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightContainer : MonoBehaviour
{
    public GameObject mesh;
    public Light[] lights;

    public void TurnOn()
    {
        mesh.SetActive(true);
        foreach (var light in lights)
        {
            light.intensity = 1;
        }
    }

    public void TurnOff()
    {
        mesh.SetActive(false);
        foreach (var light in lights)
        {
            light.intensity = 0;
        }
    }
}
