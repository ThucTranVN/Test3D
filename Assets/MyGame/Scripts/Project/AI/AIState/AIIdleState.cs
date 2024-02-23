using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIState
{
    public AIStateID GetID()
    {
        return AIStateID.Idle;
    }

    public void Enter(AIAgent agent)
    {
        agent.weapons.DeactivateWeapon();
        agent.navMeshAgent.ResetPath();
    }

    public void Exit(AIAgent agent)
    {
        
    }

    public void Update(AIAgent agent)
    {
        if (agent.playerTransform.GetComponent<Health>().IsDead()) return;

        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;
        if(playerDirection.sqrMagnitude > agent.maxSightDistance * agent.maxSightDistance)
        {
            return;
        }

        Vector3 agentDirection = agent.transform.forward;

        playerDirection.Normalize();

        float dotProduct = Vector3.Dot(playerDirection, agentDirection);

        if(dotProduct > 0)
        {
            agent.stateMachine.ChangeState(AIStateID.ChasePlayer);
        }
    }
}
