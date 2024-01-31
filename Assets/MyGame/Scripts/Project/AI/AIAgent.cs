using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public Transform playerTransform;
    public AIStateMachine stateMachine;
    public AIStateID initState;
    public NavMeshAgent navMeshAgent;
    public Ragdoll ragdoll;
    public UIHealthBar UIHealthBar;
    public float maxTime = 1f;
    public float maxDistance = 5f;
    public float dieForce = 10f;
    public float maxSightDistance = 10f;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<Ragdoll>();
        UIHealthBar = GetComponentInChildren<UIHealthBar>();
        navMeshAgent.stoppingDistance = maxDistance;
        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterState(new AIChasePlayerState());
        stateMachine.RegisterState(new AIDeathState());
        stateMachine.RegisterState(new AIIdleState());
        stateMachine.ChangeState(initState);
    }

    void Update()
    {
        stateMachine.Update();
    }
}
