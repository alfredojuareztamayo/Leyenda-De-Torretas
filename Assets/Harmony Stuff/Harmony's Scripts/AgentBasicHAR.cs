using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBasicHAR : MonoBehaviour
{
    [SerializeField] float m_maxVel, m_maxSteerForce, m_maxSpeed, m_slowingRadius, 
    m_wanderRadius, m_wanderDistance, m_wanderAngle, m_wanderAngleCR, m_pfRadius, m_leaderDist; 
    //m_wanderAngleCR STANDS FOR: WANDER ANGEL CHANGE RATE.
    //PF STANDS FOR: PATH FOLLOWING.
    [SerializeField] protected float m_eyesPerceptionRad;
    [SerializeField] protected int health;
    [SerializeField] protected ParticleSystem effect;
    [SerializeField] protected Transform m_eyesPerceptionPos;
    protected Transform m_target;
    protected Transform m_ally;
    private AgentState m_agentState;

    /// <summary>
    /// Called once per frame.
    /// Handles agent's death condition.
    /// </summary>
    void Update()
    {
        Die();
    }

    /// <summary>
    /// Gets the maximum velocity of the agent.
    /// </summary>
    /// <returns>The maximum velocity value.</returns>
    public float GetMaxVel()
    {
        return m_maxVel;
    }

    /// <summary>
    /// Gets the maximum steering force of the agent.
    /// </summary>
    /// <returns>The maximum steering force value.</returns>
    public float GetMaxSteerForce()
    {
        return m_maxSteerForce;
    }

    public float GetMaxSpeed()
    {
        return m_maxSpeed;
    }

    public float GetLeaderDist()
    {
        return m_leaderDist;
    }

    public float GetWanderRadius()
    {
        return m_wanderRadius;
    }

    public float GetPathRadius()
    {
        return m_pfRadius;
    }

    public float GetWanderDistance()
    {
        return m_wanderDistance;
    }

    public float GetWanderAngle()
    {
        return m_wanderAngle;
    }

    public float GetWanderAngleCR()
    {
        return m_wanderAngleCR;
    }

    public float GetSlowingRadius()
    {
        return m_slowingRadius;
    }

    /// <summary>
    /// Applies damage to the agent's health and triggers a particle effect.
    /// </summary>
    /// <param name="dmg">The amount of damage to apply.</param>
    public void TakeDamage(int dmg)
    {
        health -= dmg;
        effect.Play();
    }

    /// <summary>
    /// Increases the agent's health by a specified amount.
    /// </summary>
    /// <param name="heal">The amount of healing to apply.</param>
    public void ReceiveHeal(int heal)
    {
        health += heal;
    }

    /// <summary>
    /// Handles agent's death when health reaches or falls below zero.
    /// </summary>
    public void Die()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Changes the current state of the agent.
    /// </summary>
    /// <param name="newState">The new state to set.</param>
    public void ChangeAgentState(AgentState newState)
    {
        if (m_agentState == newState)
        {
            return;
        }
        m_agentState = newState;
        switch (newState)
        {
            case AgentState.None:
                break;
            case AgentState.Seeking:
                break;
            case AgentState.Fleeing:
                break;
            case AgentState.Wandering:
                break;
            case AgentState.Arriving:
                break;
            case AgentState.Pursuiting:
                break;
            case AgentState.Evading:
                break;
            case AgentState.PathFollowing:
                break;
            case AgentState.LeaderFollowing:
                break;
        }
    }

    /// <summary>
    /// Gets the current state of the agent.
    /// </summary>
    /// <returns>The current agent state.</returns>
    public AgentState GetAgentState()
    {
        return m_agentState;
    }

    /// <summary>
    /// Enumeration representing the possible states of the agent.
    /// </summary>
    public enum AgentState
    {
        None,
        Seeking,
        Fleeing,
        Wandering,
        Arriving,
        Pursuiting,
        Evading,
        PathFollowing,
        LeaderFollowing
    }
}
