using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    public Transform playerTransform;
    public float maxTime = 1f;
    public float maxDistance = 5f;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float timer = 0f;

    private int speedParam = Animator.StringToHash("Speed");
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.stoppingDistance = maxDistance;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            float sqrDistance = (playerTransform.position - navMeshAgent.destination).sqrMagnitude;
            if (sqrDistance > maxDistance * maxDistance)
            {
                navMeshAgent.destination = playerTransform.position;
            }
            timer = maxTime;
        }
        
        animator.SetFloat(speedParam, navMeshAgent.velocity.magnitude);
    }
}
