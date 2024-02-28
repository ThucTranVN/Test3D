using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        agent.DisableAll();

        DOVirtual.DelayedCall(2f, () =>
        {
            if (UIManager.HasInstance)
            {
                string message = "Win";
                UIManager.Instance.ShowPopup<PopupMessage>(data: message);
            }
        });
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
