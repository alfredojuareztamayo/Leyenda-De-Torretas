using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoorLogicHAR : AgentBasicHAR
{
    public bool player1 = false;
    public bool player2 = false;
    int p1LayerID = 10;
    int p2LayerID = 11;
    int layerMask = 1;
    bool coroutineOff = true;
    Collider[] eyesPerceived;
    public GameObject bullet;
    public Transform bulletPF;
    [SerializeField] protected int hp = 250;
    void Start()
    {
        if (gameObject.tag == "Player1")
        {
            player1 = true;
        }
        if (gameObject.tag == "Player2")
        {
            player2 = true;
        }
    }

    void Update()
    {
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
        MovementManager();
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

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
                    Debug.Log("Enemy found.");
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
            coroutineOff = true;
        }
        if (m_target != null && coroutineOff)
        {
            ActionManager();
            coroutineOff = false;
        }
    }

    void MovementManager()
    {
        switch (GetAgentState())
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
            case AgentState.PathFollowing:
                break;
        }
    }

    void ActionManager()
    {
        //Stuff like shoot, heal and so on.
        StartCoroutine(TowerShoot());
    }

    public IEnumerator TowerShoot()
    {     
        Debug.Log("Coroutine working.");
        GameObject thisB = Instantiate(bullet, bulletPF.position, Quaternion.identity);
        thisB.GetComponent<TowerBulletHAR>().SetTarget(m_target);
        if (player1 == true)
        {
            thisB.GetComponent<TowerBulletHAR>().c_player1 = true;
        }
        if (player2 == true)
        {
            thisB.GetComponent<TowerBulletHAR>().c_player2 = true;
        }
        yield return new WaitForSeconds(2f);
        coroutineOff = true;
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
}
