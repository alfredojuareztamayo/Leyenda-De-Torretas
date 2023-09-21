using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PathFollowerHAR : AgentBasicHAR
{
    public List<Vector3> waypoints = new List<Vector3>();
    Rigidbody m_rgbd;

    void Start()
    {
        m_rgbd = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Manages perception of enemies.
    /// </summary>
    void Update()
    {
        PerceptionManager();
        DecisionManager();
    }

    void FixedUpdate()
    {

    }

    /// <summary>
    /// Manages agent's decision-making based on perception.
    /// </summary>
    void PerceptionManager()
    {

    }

    void DecisionManager()
    {
        ChangeAgentState(AgentState.PathFollowing);
        MovementManager();
    }

    void MovementManager()
    {
        switch (GetAgentState())
        {
            case AgentState.None:
                break;
            case AgentState.Seeking:
                m_rgbd.velocity = SteeringBehavioursHAR.seek(transform, m_target.position);
                SteeringBehavioursHAR.lookAt(transform);
                break;
            case AgentState.Fleeing:
                break;
            case AgentState.Wandering:
                break;
            case AgentState.Arriving:
                m_rgbd.velocity = SteeringBehavioursHAR.arrival(transform, m_target.position);
                SteeringBehavioursHAR.lookAt(transform);
                break;
            case AgentState.PathFollowing:
                m_rgbd.velocity = SteeringBehavioursHAR.pathFollowing(transform, waypoints);
                SteeringBehavioursHAR.lookAt(transform);
                break;
        }
    }

    //ASSIGN PATH FOLLOWING FOR PLAYER 1'S LEFT SIDE
    public void SpawnLeftSideP1()
    {
        waypoints.Add(new Vector3(686.549988f, 0.25f, 482.809998f));
        waypoints.Add(new Vector3(609.41803f, 0.25f, 404.68399f));
        waypoints.Add(new Vector3(592.469971f, 0.25f, 398.970001f));
        waypoints.Add(new Vector3(407.23999f, 0.25f, 397.579987f));
        waypoints.Add(new Vector3(391.450012f, 0.25f, 404.980011f));
        waypoints.Add(new Vector3(305.369995f, 0.25f, 489.529999f));
    }

    //ASSIGN PATH FOLLOWING FOR PLAYER 1'S RIGHT SIDE
    public void SpawnRightSideP1()
    {
        waypoints.Add(new Vector3(686.549988f, 0.25f, 522.72998f));
        waypoints.Add(new Vector3(610.150024f, 0.25f, 594.780029f));
        waypoints.Add(new Vector3(592.26001f, 0.25f, 600.080017f));
        waypoints.Add(new Vector3(411.119995f, 0.25f, 600.01001f));
        waypoints.Add(new Vector3(390.559998f, 0.25f, 595.030029f));
        waypoints.Add(new Vector3(305.369995f, 0.25f, 511.529999f));
    }

    //ASSIGN PAATH FOLLOWWING FOR PLAYER 2'S LEFT SIDE
    public void SpawnRightSideP2()
    {
        waypoints.Add(new Vector3(305.369995f, 0.25f, 489.529999f));
        waypoints.Add(new Vector3(391.450012f, 0.25f, 404.980011f));
        waypoints.Add(new Vector3(407.23999f, 0.25f, 397.579987f));
        waypoints.Add(new Vector3(592.469971f, 0.25f, 398.970001f));
        waypoints.Add(new Vector3(609.41803f, 0.25f, 404.68399f));
        waypoints.Add(new Vector3(686.549988f, 0.25f, 482.809998f));
    }

    //ASSIGN PATH FOLLOWING FOR PLAYER 2'S RIGHT SIDE
    public void SpawnLeftSideP2()
    {
        waypoints.Add(new Vector3(305.369995f, 0.25f, 511.529999f));
        waypoints.Add(new Vector3(390.559998f, 0.25f, 595.030029f));
        waypoints.Add(new Vector3(411.119995f, 0.25f, 600.01001f));
        waypoints.Add(new Vector3(592.26001f, 0.25f, 600.080017f));
        waypoints.Add(new Vector3(610.150024f, 0.25f, 594.780029f));
        waypoints.Add(new Vector3(686.549988f, 0.25f, 522.72998f));
    }
}
