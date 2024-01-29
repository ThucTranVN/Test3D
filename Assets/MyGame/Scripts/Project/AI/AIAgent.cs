using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public AIStateMachine stateMachine;
    public AIStateID initState;
    public NavMeshAgent navMeshAgent;
    public Ragdoll ragdoll;
    public UIHealthBar UIHealthBar;
    public float maxTime = 1f;
    public float maxDistance = 5f;
    public float dieForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<Ragdoll>();
        UIHealthBar = GetComponentInChildren<UIHealthBar>();
        navMeshAgent.stoppingDistance = maxDistance;
        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterState(new AIChasePlayerState());
        stateMachine.RegisterState(new AIDeathState());
        stateMachine.ChangeState(initState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
