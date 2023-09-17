using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAgent : MonoBehaviour
{
    BasicStatsAgent statsAgent;
   // Arrow arrowManager;
    TypeAgent agentType;
    void Start()
    {
       // arrowManager = GetComponent<Arrow>();
        agentType = TypeAgent.Shooter;
        statsAgent = GetComponent<BasicStatsAgent>();
        statsAgent.SelectAgentType(agentType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
