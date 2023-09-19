using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SteeringBHAlfredoArrow 
{
    public static Vector3 Seek(Transform agent, Vector3 target)
    {
        ArrowTower arrowTower = agent.GetComponent<ArrowTower>();
        //AgentBasic agentBasic = agent.GetComponent<AgentBasic>();
        Rigidbody agentRB = agent.GetComponent<Rigidbody>();

        Vector3 desiredVel = target - agent.position;
        desiredVel.Normalize();
        desiredVel *= arrowTower.MaxVel();
        Vector3 steering = desiredVel - agentRB.velocity;
        steering = Truncate(steering, arrowTower.SteeringBehaviour());
        steering /= agentRB.mass;
        steering += agentRB.velocity;
        steering = Truncate(steering, arrowTower.MaxSpeed());
        steering.y = 0;
        return steering;
    }

    private static Vector3 Truncate(Vector3 vector, float maxValue)
    {
        if (vector.magnitude <= maxValue)
        {
            return vector;
        }
        vector.Normalize();
        return vector *= maxValue;
    }
}
