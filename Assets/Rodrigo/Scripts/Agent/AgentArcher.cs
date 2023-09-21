using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AgentArcher : AgentBasic
{
    [SerializeField] List<Transform> m_pathsList = new List<Transform>();
    List<Transform> m_pathsListAux = new List<Transform>();

    // Start is called before the first frame update
    void Start() {
        m_healt = m_maxHealt;
        m_rb = GetComponent<Rigidbody>();
        changeAgentState(AgentState.PathFollowing);
    }

    // Update is called once per frame
    void Update() {
        PerceptionManager();
        DecisionManager();
        MovementManager();
        ActionManager();
    }

    /// <summary>
    /// Manages agent's decision-making based on perception.
    /// </summary>
    void PerceptionManager() {
        if(m_pathsListAux.Count == 0) {
            clonePathList();
        }

        m_objectsPercibed = Physics.OverlapSphere(m_eyesPerceptionPos.position , m_eyesPerceptionRad);

        foreach(Collider c in m_objectsPercibed) {

            if(c.gameObject.tag == "Blue" || c.gameObject.tag == "Green") {
                if(m_target != null && (m_target.tag == "Blue" || m_target.tag == "Green")) {
                    return;
                }
                m_target = c.transform;
                return;
            }
            m_target = null;
        }
    }

    void clonePathList() {
        foreach(Transform t in m_pathsList) {
            m_pathsListAux.Add(t);
        }
    }

    void DecisionManager() {
        if(m_healt <= 0) {
            changeAgentState(AgentState.Die);
            return;
        }

        if(m_target != null) {

            switch(m_target.tag) {
                case "Green":
                case "Blue":
                    changeAgentState(AgentState.Evade);
                    break;
                default:
                    changeAgentState(AgentState.PathFollowing);
                    break;
            }
        } else {
            changeAgentState(AgentState.PathFollowing);
        }
    }
    void MovementManager() {

        m_animator.SetFloat("Speed" , m_rb.velocity.magnitude);
        switch(getAgentState()) {
            case AgentState.None:
                break;
            case AgentState.Seek:
                m_rb.velocity = SteeringBehaviours.seek(transform , m_target.position);
                SteeringBehaviours.lookAt(transform);
                break;
            case AgentState.Flee:
                break;
            case AgentState.Wander:
                break;
            case AgentState.Arrive:
                m_rb.velocity = SteeringBehaviours.arrival(transform , m_target.position);
                SteeringBehaviours.lookAt(transform);
                break;
            case AgentState.PathFollowing:
                m_rb.velocity = SteeringBehaviours.pathFollowing(transform , m_pathsListAux);
                SteeringBehaviours.lookAt(transform);
                break;
            case AgentState.Evade:
                m_rb.velocity = SteeringBehaviours.evade(transform , m_target);
                SteeringBehaviours.lookAt(transform);
                break;
            case AgentState.Die:
                m_rb.velocity = Vector3.zero;
                break;

        }
    }

    void ActionManager() {
        switch(getAgentState()) {
            case AgentState.Die:
                m_animator.SetBool("Die" , true);
                break;
            case AgentState.Attack:
                m_animator.SetBool("attack" , true);
                break;
            default:
                m_animator.SetBool("attack" , false);
                break;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_eyesPerceptionPos.position , m_eyesPerceptionRad);
    }
}
