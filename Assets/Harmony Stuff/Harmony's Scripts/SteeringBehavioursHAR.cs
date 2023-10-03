using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavioursHAR
{
    /// <summary>
    /// Calculates steering force for seeking a target.
    /// </summary>
    /// <param name="agent">The agent's transform.</param>
    /// <param name="target">The target's transform.</param>
    /// <returns>The steering force for seeking behavior.</returns>
    public static Vector3 seek(Transform agent, Vector3 target)
    {
        AgentBasicHAR agentBasic = agent.GetComponent<AgentBasicHAR>();
        Rigidbody agentRB = agent.GetComponent<Rigidbody>();

        Vector3 desiredVel = target - agent.position;
        desiredVel.Normalize();
        desiredVel *= agentBasic.GetMaxVel();
        Vector3 steering = desiredVel - agentRB.velocity;
        steering = Truncate(steering, agentBasic.GetMaxSteerForce());
        steering /= agentRB.mass;
        steering += agentRB.velocity;
        steering = Truncate(steering, agentBasic.GetMaxSpeed());
        steering.y = 0;
        return steering;
    }

    public static Vector3 arrival(Transform agent, Vector3 target)
    {
        //AgentBasic targetBasic = target.GetComponent<AgentBasic>();
        float dist = FormulesHAR.Dist(agent.position, target);
        if (dist < 6)
        {
            Debug.Log("Slowing Down");
            AgentBasicHAR agentBasic = agent.GetComponent<AgentBasicHAR>();
            Rigidbody agentRB = agent.GetComponent<Rigidbody>();

            Vector3 desiredVel = target - agent.position;
            desiredVel = FormulesHAR.Normalize(desiredVel) * agentBasic.GetMaxVel() * (dist / 6);
            Vector3 steering = desiredVel - agentRB.velocity;
            steering = Truncate(steering, agentBasic.GetMaxSteerForce());
            steering /= agentRB.mass;
            steering += agentRB.velocity;
            steering = Truncate(steering, agentBasic.GetMaxSpeed());
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
        AgentBasicHAR agentBasic = agent.GetComponent<AgentBasicHAR>();
        Rigidbody agentRB = agent.GetComponent<Rigidbody>();

        float wanderAngle = agentBasic.GetWanderAngle();
        float wanderAngleCR = agentBasic.GetWanderAngleCR();
        float wanderRadius = agentBasic.GetWanderRadius();

        wanderAngle += Random.Range(-wanderAngleCR, wanderAngleCR);
        Vector3 circleCenter = agentRB.velocity.normalized * agentBasic.GetWanderDistance() + agent.position;
        Vector3 displacement = new Vector3(Mathf.Cos(wanderAngle), 0, Mathf.Sin(wanderAngle)) * wanderRadius;
        Vector3 wanderResult = circleCenter + displacement;
        return seek(agent, wanderResult);
    }

    public static Vector3 wander2(Transform agent)
    {
        AgentBasicHAR agentBasic = agent.GetComponent<AgentBasicHAR>();
        Rigidbody agentRB = agent.GetComponent<Rigidbody>();

        float wanderAngle = agentBasic.GetWanderAngle();
        float wanderAngleCR = agentBasic.GetWanderAngleCR();
        float wanderRadius = agentBasic.GetWanderRadius();

        Vector3 circleCenter = agentRB.velocity;
        circleCenter.Normalize();
        circleCenter *= agentBasic.GetWanderDistance();

        Vector3 displacement = new Vector3(0, 0, -1);
        displacement *= wanderRadius;

        wanderAngle += Random.Range(-wanderAngleCR, wanderAngleCR)*.5f;
        setAngle(displacement, wanderAngle);

        Vector3 wanderForce = circleCenter+displacement;
        return seek(agent, wanderForce);
    }

    public static void setAngle(Vector3 v, float n)
    {
        float len = v.magnitude;
        v.x = Mathf.Cos(n) * len;
        v.z = Mathf.Sin(n) * len;
    }

    public static Vector3 pursuit(Transform agent, Transform target)
    {
        AgentBasicHAR targetBasic = target.GetComponent<AgentBasicHAR>();
        Rigidbody targetRB = target.GetComponent<Rigidbody>();
        float maxVel = targetBasic.GetMaxVel();
        float dist = FormulesHAR.Dist(agent.position, target.position);

        float value = dist / maxVel;
        Vector3 futurePos = target.position + targetRB.velocity * value;
        return seek(agent, futurePos);
    }

    public static Vector3 evade(Transform agent, Transform target)
    {
        AgentBasicHAR targetBasic = target.GetComponent<AgentBasicHAR>();
        Rigidbody targetRB = target.GetComponent<Rigidbody>();
        float maxVel = targetBasic.GetMaxVel();
        float dist = FormulesHAR.Dist(agent.position, target.position);

        float value = dist / maxVel;
        Vector3 futurePos = target.position + targetRB.velocity * value;
        return flee(agent, futurePos);
    }

    public static Vector3 pathFollowing(Transform agent, List<Vector3> waypoints)
    {
        AgentBasicHAR agentBasic = agent.GetComponent<AgentBasicHAR>();
        Rigidbody agentRB = agent.GetComponent<Rigidbody>(); 

        if(waypoints.Count == 0)
        {
            return Vector3.zero;
        }

        Vector3 currentWaypoint = waypoints[0];
        float distToWP = FormulesHAR.Dist(agent.position, currentWaypoint);

        if(distToWP < agentBasic.GetPathRadius())
        {
            waypoints.RemoveAt(0);

            if(waypoints.Count == 0)
            {
                return Vector3.zero;
            }
            currentWaypoint = waypoints[0];
        }

        return seek(agent, currentWaypoint);
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

    /// <summary>
    /// Calculates steering force for fleeing from a target.
    /// </summary>
    /// <param name="agent">The agent's transform.</param>
    /// <param name="target">The target's transform.</param>
    /// <returns>The steering force for fleeing behavior.</returns>
    public static Vector3 flee(Transform agent, Vector3 target)
    {
        Vector3 fleeing = agent.position - target;
        return seek(agent, agent.position + fleeing);
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
        AgentBasicHAR targetBasic = target.GetComponent<AgentBasicHAR>();
        Rigidbody targetRB = target.GetComponent<Rigidbody>();
        Vector3 copyVel = target.GetComponent<Rigidbody>().velocity;
        Vector3 nv = new Vector3(copyVel.x, copyVel.y, copyVel.z);

        nv = targetRB.velocity * -1;
        nv.Normalize();
        nv *= targetBasic.GetLeaderDist();
        Vector3 behind = target.position + nv;
        Vector3 force = arrival(agent, behind);
        return force;
    }
}

