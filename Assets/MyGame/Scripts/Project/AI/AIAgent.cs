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
    public AIWeapons weapons;
    public float maxTime;
    public float maxDistance;
    public float dieForce;
    public float maxSightDistance;

    private void Awake()
    {
        if (DataManager.HasInstance)
        {
            maxTime = DataManager.Instance.DataConfig.MaxTime;
            maxDistance = DataManager.Instance.DataConfig.MaxDistance;
            dieForce = DataManager.Instance.DataConfig.DieForce;
            maxSightDistance = DataManager.Instance.DataConfig.MaxSightDistance;
        }
    }

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<Ragdoll>();
        UIHealthBar = GetComponentInChildren<UIHealthBar>();
        weapons = GetComponent<AIWeapons>();
        navMeshAgent.stoppingDistance = maxDistance;
        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterState(new AIChasePlayerState());
        stateMachine.RegisterState(new AIDeathState());
        stateMachine.RegisterState(new AIIdleState());
        stateMachine.RegisterState(new AIFIndWeaponState());
        stateMachine.RegisterState(new AIAttackPlayerState());
        stateMachine.ChangeState(initState);
    }

    void Update()
    {
        stateMachine.Update();
    }

    public void DisableAll()
    {
        var allComponents = GetComponents<MonoBehaviour>();

        foreach (var comp in allComponents)
        {
            comp.enabled = false;
        }

        navMeshAgent.enabled = false;
    }
}
