using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTest : MonoBehaviour
{
    [SerializeField]
    private float maximumForce;

    [SerializeField]
    private float maxiumForceTime;

    private float timeMouseButtonDown;

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            timeMouseButtonDown = Time.time; //5s
        }

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                ZombieTest zombie = hitInfo.collider.GetComponentInParent<ZombieTest>();

                if(zombie != null)
                {
                    float mouseButtonDownDuration = Time.time - timeMouseButtonDown;
                    float forcePercentage = mouseButtonDownDuration / maxiumForceTime;
                    float forceMagnitude = Mathf.Lerp(1, maximumForce, forcePercentage);

                    Vector3 forceDirection = zombie.transform.position - mainCamera.transform.position;
                    forceDirection.y = 1;
                    forceDirection.Normalize();

                    Vector3 force = forceMagnitude * forceDirection;

                    zombie.TriggerRagdoll(force, hitInfo.point);
                }
            }
        }
    }
}
