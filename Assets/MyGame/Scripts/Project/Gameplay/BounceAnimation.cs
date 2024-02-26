using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAnimation : MonoBehaviour
{
    private float bounceSpeed;
    private float bounceAmplitude;
    private float rotationSpeed;
    private float staringHeight;
    private float timeOffset;

    private void Awake()
    {
        if (DataManager.HasInstance)
        {
            bounceSpeed = DataManager.Instance.DataConfig.BounceSpeed;
            bounceAmplitude = DataManager.Instance.DataConfig.BounceAmplitude;
            rotationSpeed = DataManager.Instance.DataConfig.RotationSpeed;
        }
    }

    void Start()
    {
        staringHeight = transform.localPosition.y;
        timeOffset = Random.value * Mathf.PI * 2;
    }

    void Update()
    {
        //Bounce
        float finalHeight = staringHeight + Mathf.Sin(Time.time * bounceSpeed + timeOffset) * bounceAmplitude;
        var position = transform.localPosition;
        position.y = finalHeight;
        transform.localPosition = position;

        //Spin
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotation.y += rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
}
