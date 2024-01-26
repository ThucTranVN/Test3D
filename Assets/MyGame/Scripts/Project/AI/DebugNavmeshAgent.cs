using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DebugNavmeshAgent : MonoBehaviour
{
    public bool isShowVelocity;
    public bool isShowDesiredVelocity;
    public bool isShowPath;
    public Color velocityColor;
    public Color desiredVelocityColor;
    public Color pathColor;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnDrawGizmos()
    {
        if (isShowVelocity)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + agent.velocity);
        }

        if (isShowDesiredVelocity)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + agent.desiredVelocity);
        }

        if (isShowPath)
        {
            Gizmos.color = pathColor;
            var agentPath = agent.path;
            Vector3 prevCornor = transform.position;
            foreach (var conor in agentPath.corners)
            {
                Gizmos.DrawLine(prevCornor, conor);
                Gizmos.DrawSphere(conor, 0.1f);
                prevCornor = conor;
            }
        }

    }
}
