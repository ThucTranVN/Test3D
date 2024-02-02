using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeathState : AIState
{
    public Vector3 direction;

    public void Enter(AIAgent agent)
    {
        if (agent.ragdoll)
        {
            agent.ragdoll.ActiveRagdoll();
            direction.y = 1f;
            agent.ragdoll.ApplyFore(direction * agent.dieForce);
        }

        agent.UIHealthBar.Deactive();
        agent.weapons.DropWeapon();
    }

    public void Exit(AIAgent agent)
    {
        
    }

    public AIStateID GetID()
    {
        return AIStateID.Death;
    }

    public void Update(AIAgent agent)
    {
        
    }
}
