using Unity.Burst;
using UnityEngine;

/// <summary>
/// Enumeration representing the possible states of the agent.
/// </summary> 
public enum AgentState {
    None,
    Seek,
    Flee,
    Wander,
    Arrive,
    Pursuit,
    Evade,
    PathFollowing,
    LeaderFollowing,
    Die,
    Attack
}

/// <summary>
/// Represents a basic agent with movement and perception capabilities.
/// </summary>
/// 
[RequireComponent(typeof(Rigidbody))]
public class AgentBasic : MonoBehaviour {
    private AgentState m_agentState;
    protected Rigidbody m_rb;
    [Min(0)]
    [SerializeField] protected float m_maxHealt;
    [SerializeField] protected float m_healt;
    [SerializeField] float m_maxVel;
    [SerializeField] float m_maxSteeringForce;
    [SerializeField] float m_maxSpeed;
    [SerializeField] float m_arriveDistance;
    [SerializeField] float m_wanderRadius;
    [SerializeField] float m_wanderDistance;
    [SerializeField] float m_wanderDelay;
    public float m_wanderDelayAux;
    [SerializeField] float m_pathArriveDistance;
    [SerializeField] float m_leaderDistance;
    [SerializeField] protected float m_eyesPerceptionRad;
    [SerializeField] protected Transform m_eyesPerceptionPos;
    protected Transform m_target;
    protected Transform m_ally;
    protected Vector3 m_wanderTarget = Vector3.zero;
    protected Collider[] m_objectsPercibed;
    [SerializeField] protected Animator m_animator;

    public void setDamage(float damage) {
        m_healt -= damage;
    }
    /// <summary>
    /// Gets the maximum velocity of the agent.
    /// </summary>
    /// <returns>The maximum velocity value.</returns>
    public float getMaxVel() {
        return m_maxVel;
    }
    /// <summary>
    /// Gets the maximum steering force of the agent.
    /// </summary>
    /// <returns>The maximum steering force value.</returns>
    public float getMaxSteerForce() {
        return m_maxSteeringForce;
    }
    public float getMaxSpeed() {
        return m_maxSpeed;
    }
    public float getArriveDistance() {
        return m_arriveDistance;
    }
    public float getWanderRadius() {
        return m_wanderRadius;
    }
    public float getWanderDistance() {
        return m_wanderDistance;
    }
    public float getWanderDelay() {
        return m_wanderDelay;
    }
    public float getPathArriveDistance() {
        return m_pathArriveDistance;
    }
    public float getLeaderDistance() {
        return m_leaderDistance;
    }
    /// <summary>
    /// Gets the current state of the agent.
    /// </summary>
    /// <returns>The current agent state.</returns>
    public AgentState getAgentState() {
        return m_agentState;
    }
    public Vector3 getWanderTarget() {
        return m_wanderTarget;
    }
    public void setWanderTarget(Vector3 wanderTarget) {
        m_wanderTarget = wanderTarget;
    }
    /// <summary>
    /// Changes the current state of the agent.
    /// </summary>
    /// <param name="newState">The new state to set.</param>
    protected void changeAgentState(AgentState newState) {
        if(m_agentState == newState) {
            return;
        }
        m_agentState = newState;
    }
}
