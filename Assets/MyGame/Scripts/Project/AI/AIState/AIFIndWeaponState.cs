using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFIndWeaponState : AIState
{
    public AIStateID GetID()
    {
        return AIStateID.FindWeapon;
    }

    public void Enter(AIAgent agent)
    {
        WeaponPickup pickup = FindClosetWeapon(agent);
        if (pickup)
        {
            agent.navMeshAgent.destination = pickup.transform.position;
            agent.navMeshAgent.stoppingDistance = 0f;
            agent.navMeshAgent.speed = 5f;
        }
    }

    public void Exit(AIAgent agent)
    {
       
    }

    public void Update(AIAgent agent)
    {
        if (agent.weapons.HasWeapon())
        {
            agent.weapons.ActivateWeapon();
        }
    }

    private WeaponPickup FindClosetWeapon(AIAgent agent)
    {
        WeaponPickup[] weapons = Object.FindObjectsOfType<WeaponPickup>();
        WeaponPickup closetWeapon = null;
        float closestDistance = float.MaxValue;
        foreach (var weapon in weapons)
        {
            float distanceToWeapon = Vector3.Distance(agent.transform.position, weapon.transform.position);
            if(distanceToWeapon < closestDistance)
            {
                closestDistance = distanceToWeapon;
                closetWeapon = weapon;
            }
        }

        return closetWeapon;
    }
}
