using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PathFollowerHAR : AgentBasicHAR
{
    public List<Vector3> waypoints = new List<Vector3>();
    Rigidbody m_rgbd;
    Collider[] eyesPerceived;
    int p1LayerID = 10;
    int p2LayerID = 11;
    int layerMask = 1;
    bool inRange = false;
    bool coroutineOff = true;
    public bool player1 = false;
    public bool player2 = false;
    int dmg = 1;
    public GameObject wpnArea;
    PathFollowerWeaponHAR wpn;
    void Start()
    {
        m_rgbd = GetComponent<Rigidbody>();

        if (player1 == true)
        {
            gameObject.tag = "Player1";
            gameObject.layer = LayerMask.NameToLayer("Player1");
        }
        if (player2 == true)
        {
            gameObject.tag = "Player2";
            gameObject.layer = LayerMask.NameToLayer("Player2");
        }
    }

    /// <summary>
    /// Manages perception of enemies.
    /// </summary>
    void Update()
    {
        Debug.Log(inRange);
        if (player1 == true)
        {
            eyesPerceived = Physics.OverlapSphere(m_eyesPerceptionPos.position, m_eyesPerceptionRad, layerMask << p2LayerID);
        }
        if (player2 == true)
        {
            eyesPerceived = Physics.OverlapSphere(m_eyesPerceptionPos.position, m_eyesPerceptionRad, layerMask << p1LayerID);
        }
        PerceptionManager();
        DecisionManager();
        InRangeBehaviour();
        wpn = GetComponentInChildren<PathFollowerWeaponHAR>();
        if (wpn == null)
        {
            return;
        }
        else
        {
            if (wpn.collidersInRange.Count == 0)
            {
                inRange = false;
            }
        }
    }

    /// <summary>
    /// Manages agent's decision-making based on perception.
    /// </summary>
    void PerceptionManager()
    {
        if (eyesPerceived == null)
        {
            return;
        }
        if (eyesPerceived != null)
        {
            foreach (Collider eyesP in eyesPerceived)
            {
                if (eyesP.tag == "Player2" || eyesP.tag == "Player1")
                {
                    m_target = eyesP.transform;
                }
            }
            if (eyesPerceived.Length == 0)
            {
                m_target = null;
            }
        }
        eyesPerceived = null;
    }

    void DecisionManager()
    {
        if (m_target == null)
        {
            ChangeAgentState(AgentState.PathFollowing);
            MovementManager();
        }
        if (m_target != null)
        {
            if (inRange)
            {
                ChangeAgentState(AgentState.None);
                MovementManager();
                if (coroutineOff)
                {
                    ActionManager();
                    coroutineOff = false;
                }
            }
            else
            {
                Debug.Log("I am the one who seeks!");
                coroutineOff = true;
                ChangeAgentState(AgentState.Seeking);
                MovementManager();
            }
        }
    }

    void MovementManager()
    {
        switch (GetAgentState())
        {
            case AgentState.None:
                Debug.Log("I am gonna stop.");
                this.transform.LookAt(m_target);
                break;
            case AgentState.Seeking:
                Debug.Log("I am gonna seek.");
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

    void ActionManager()
    {
        //Stuff like shoot, heal and so on.
        StartCoroutine(MeeleAttack());
    }

    public IEnumerator MeeleAttack()
    {
        while (inRange)
        {
            wpnArea.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            wpnArea.SetActive(false);
            yield return new WaitForSeconds(1.5f);
        }
    }

    public void InRangeBehaviour()
    {
        if (m_target == null)
        {
            return;
        }
        float dist = FormulesHAR.Dist(transform.position, m_target.transform.position);
        //Debug.Log(dist);
        if (dist < 3)
        {
            inRange = true;
            return;

        }
        inRange = false;
    }

    private void OnDrawGizmos()
    {
        if (player1 == true)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(m_eyesPerceptionPos.position, m_eyesPerceptionRad);
        }
        if (player2 == true)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(m_eyesPerceptionPos.position, m_eyesPerceptionRad);
        }
    }

    //OBSOLETE
    /*
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player2" && player1 == true)
        {
            AgentBasicHAR target = other.GetComponent<AgentBasicHAR>();
            TowerPoorLogicHAR tower = other.GetComponent<TowerPoorLogicHAR>();
            if (target)
            {
                target.TakeDamage(dmg);
            }
            if (tower)
            {
                tower.TakeDamage(dmg);
            }

        }
        if (other.tag == "Player1" && player2 == true)
        {
            AgentBasicHAR target = other.GetComponent<AgentBasicHAR>();
            TowerPoorLogicHAR tower = other.GetComponent<TowerPoorLogicHAR>();
            if (target)
            {
                target.TakeDamage(dmg);
            }
            if (tower)
            {
                tower.TakeDamage(dmg);
            }
        }
    }
    */

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
