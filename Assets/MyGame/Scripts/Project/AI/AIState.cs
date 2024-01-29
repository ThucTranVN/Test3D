using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AIState
{
    AIStateID GetID();
    void Enter(AIAgent agent);
    void Update(AIAgent agent);
    void Exit(AIAgent agent);
}
