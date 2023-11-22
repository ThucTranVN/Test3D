using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieTest : MonoBehaviour
{
    private enum ZombieState
    {
        Walking,
        Ragdoll
    }

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private PhysicMaterial zombiePhysicMaterial;

    private Rigidbody[] zombieRBs;
    private CharacterJoint[] zombieJoints;
    private Collider[] zombieColliders;

    private ZombieState currentState = ZombieState.Walking;
    private Animator zombieAnimator;
    private CharacterController zombieCC;

    private void Awake()
    {
        zombieRBs = GetComponentsInChildren<Rigidbody>();
        zombieJoints = GetComponentsInChildren<CharacterJoint>();
        zombieColliders = GetComponentsInChildren<Collider>();
        zombieAnimator = GetComponent<Animator>();
        zombieCC = GetComponent<CharacterController>();
        DisableRagdoll();
        SetUpCharacterJoints();
        SetUpCollider();
    }

    void Update()
    {
        switch (currentState)
        {
            case ZombieState.Walking:
                WalkingBehaviour();
                break;
            case ZombieState.Ragdoll:
                RagdollBehaviour();
                break;
        }
    }

    private void SetUpCollider()
    {
        foreach (var col in zombieColliders)
        {
            col.material = zombiePhysicMaterial;
        }
    }

    private void SetUpCharacterJoints()
    {
        foreach (var joint in zombieJoints)
        {
            joint.enableProjection = true;
        }
    }

    private void DisableRagdoll()
    {
        foreach (var rigidbody in zombieRBs)
        {
            rigidbody.isKinematic = true;
        }

        zombieAnimator.enabled = true;
        zombieCC.enabled = true;
    }

    private void EnableRagdoll()
    {
        foreach (var rigidbody in zombieRBs)
        {
            rigidbody.isKinematic = false;
        }

        zombieAnimator.enabled = false;
        zombieCC.enabled = false;
    }

    private void WalkingBehaviour()
    {
        Vector3 direction = mainCamera.transform.position - transform.position;
        direction.y = 0;
        direction.Normalize();

        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 20 * Time.deltaTime);
    }

    private void RagdollBehaviour()
    {
        this.enabled = false;
    }

    public void TriggerRagdoll(Vector3 force, Vector3 hitPoint)
    {
        EnableRagdoll();

        //Rigidbody hitRb = zombieRBs.OrderBy(rigidbody => Vector3.Distance(rigidbody.position, hitPoint)).First();
        Rigidbody hitRb = FindHitRigidbody(hitPoint);
        print($"Name: {hitRb.gameObject.name}");
        hitRb.AddForceAtPosition(force, hitPoint, ForceMode.Impulse);
        currentState = ZombieState.Ragdoll;
    }

    private Rigidbody FindHitRigidbody(Vector3 hitPoint)
    {
        Rigidbody closestRigidbody = null;
        float closestDistance = 0;

        foreach (var rb in zombieRBs)
        {
            float distance = Vector3.Distance(rb.position, hitPoint);

            if(closestRigidbody == null || distance < closestDistance)
            {
                closestDistance = distance;
                closestRigidbody = rb;
            }
        }

        return closestRigidbody;
    }
}
