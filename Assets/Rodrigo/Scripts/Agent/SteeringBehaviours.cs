using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides steering behaviors for agent movement.
/// </summary>
public class SteeringBehaviours
{
    /// <summary>
    /// Calculates steering force for seeking a target.
    /// </summary>
    /// <param name="agent">The agent's transform.</param>
    /// <param name="target">The target's transform.</param>
    /// <returns>The steering force for seeking behavior.</returns>
    public static Vector3 seek(Transform agent, Vector3 target)
    {
        AgentBasic agentBasic = agent.GetComponent<AgentBasic>();
        Rigidbody agentRB = agent.GetComponent<Rigidbody>();

        Vector3 desiredVel = target - agent.position;
        desiredVel.Normalize();
        desiredVel *= agentBasic.getMaxVel();
        Vector3 steering = desiredVel - agentRB.velocity;
        steering = Truncate(steering, agentBasic.getMaxSteerForce());
        steering /= agentRB.mass;
        steering += agentRB.velocity;
        steering = Truncate(steering, agentBasic.getMaxSpeed());
        steering.y = 0;
        return steering;
    }

    /// <summary>
    /// Calculates steering force for fleeing from a target.
    /// </summary>
    /// <param name="agent">The agent's transform.</param>
    /// <param name="target">The target's transform.</param>
    /// <returns>The steering force for fleeing behavior.</returns>
    public static Vector3 flee(Transform agent , Vector3 target) {
        Vector3 fleeing = agent.position - target;
        return seek(agent , agent.position + fleeing);
    }



    public static Vector3 arrival(Transform agent, Vector3 target)
    {
        //AgentBasic targetBasic = target.GetComponent<AgentBasic>();
        float dist = Vector3.Distance(agent.position, target);
        if (dist < 6)
        {
            Debug.Log("Slowing Down");
            AgentBasic agentBasic = agent.GetComponent<AgentBasic>();
            Rigidbody agentRB = agent.GetComponent<Rigidbody>();

            Vector3 desiredVel = target - agent.position;
            desiredVel = Vector3.Normalize(desiredVel) * agentBasic.getMaxVel() * (dist / 6);
            Vector3 steering = desiredVel - agentRB.velocity;
            steering = Truncate(steering, agentBasic.getMaxSteerForce());
            steering /= agentRB.mass;
            steering += agentRB.velocity;
            steering = Truncate(steering, agentBasic.getMaxSpeed());
            steering.y = 0;
            Debug.Log(desiredVel);
            return steering;
        }
        else
        {
            Debug.Log("Regular seeking");
            Vector3 steering = seek(agent, target);
            return steering;
        }
    }

    public static Vector3 wander(Transform agent)
    {
        AgentBasic agentBasic = agent.GetComponent<AgentBasic>();
        
        if(agentBasic.m_wanderDelayAux <= 0f) {
            Vector3 wanderTarget = agentBasic.getWanderTarget();
            wanderTarget = agent.position;
            wanderTarget += agent.TransformDirection(Vector3.forward) * agentBasic.getWanderDistance();
            float limit = agentBasic.getWanderRadius();
            wanderTarget.x += Random.Range(-limit , limit);
            wanderTarget.z += Random.Range(-limit , limit);
            agentBasic.setWanderTarget(wanderTarget);
            agentBasic.m_wanderDelayAux = agentBasic.getWanderDelay();
        }
        agentBasic.m_wanderDelayAux -= Time.deltaTime;


        return seek(agent, agentBasic.getWanderTarget());
    }

    static Vector3 predictedPosition(Transform agent , Transform target) {
        AgentBasic targetBasic = target.GetComponent<AgentBasic>();
        Rigidbody targetRB = target.GetComponent<Rigidbody>();
        float distance = Vector3.Distance(agent.position , target.position);
        float value = distance / targetBasic.getMaxVel();
        Vector3 predictedPosition = target.position + targetRB.velocity * value;
        return predictedPosition;
    }

    public static Vector3 pursuit(Transform agent, Transform target)
    {
        return seek(agent, predictedPosition(agent , target));
    }

    public static Vector3 evade(Transform agent, Transform target)
    {
        return flee(agent, predictedPosition(agent , target));
    }

    public static Vector3 pathFollowing(Transform agent, List<Transform> pathsList)
    {
        AgentBasic agentBasic = agent.GetComponent<AgentBasic>();

        if(pathsList.Count == 0)
        {
            return Vector3.zero;
        }

        Vector3 target = pathsList[0].position;
        float distanceToCurrentPath = Vector3.Distance(agent.position, target);

        if(distanceToCurrentPath < agentBasic.getPathArriveDistance())
        {
            pathsList.RemoveAt(0);
        }

        return seek(agent, target);
    }

    public static void lookAt(Transform agent)
    {
        Vector3 copyVel = agent.GetComponent<Rigidbody>().velocity;
        Vector3 agentRBVel = new Vector3(copyVel.x, copyVel.y, copyVel.z);
        agentRBVel.Normalize();
        agentRBVel *= 2;
        Vector3 toLook = new Vector3(
            agentRBVel.x + agent.position.x,
            agent.position.y,
            agentRBVel.z + agent.position.z
        );
        agent.transform.LookAt(toLook);
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




    public static Vector3 followLeader(Transform agent, Transform target)
    {
        AgentBasic targetBasic = target.GetComponent<AgentBasic>();
        Rigidbody targetRB = target.GetComponent<Rigidbody>();
        Vector3 copyVel = target.GetComponent<Rigidbody>().velocity;
        Vector3 nv = new Vector3(copyVel.x, copyVel.y, copyVel.z);

        nv = targetRB.velocity * -1;
        nv.Normalize();
        nv *= targetBasic.getLeaderDistance();
        Vector3 behind = target.position + nv;
        Vector3 force = arrival(agent, behind);
        return force;
    }
}
