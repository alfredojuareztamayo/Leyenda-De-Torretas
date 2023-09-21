using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHAR : MonoBehaviour
{
    public GameObject agentPF;
    public GameObject p1spawnPoint;
    public GameObject p2spawnPoint;
    float cd = 1;
    float currentP1TimeRight;
    float currentP1TimeLeft;
    float currentP2TimeRight;
    float currentP2TimeLeft;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentP1TimeLeft += Time.deltaTime;
        currentP1TimeRight += Time.deltaTime;
        currentP2TimeLeft += Time.deltaTime;
        currentP2TimeRight += Time.deltaTime;

        //SPAWN P1'S LEFT SIDE
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentP1TimeLeft > cd)
            {
                GameObject agent = Instantiate(agentPF, p1spawnPoint.transform.position, Quaternion.identity);
                PathFollowerHAR agentSc = agent.GetComponent<PathFollowerHAR>();
                agentSc.SpawnLeftSideP1();
                currentP1TimeLeft = 0;
            }
        }

        //SPAWN P1'S RIGHT SIDE
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (currentP1TimeRight > cd)
            {
                GameObject agent = Instantiate(agentPF, p1spawnPoint.transform.position, Quaternion.identity);
                PathFollowerHAR agentSc = agent.GetComponent<PathFollowerHAR>();
                agentSc.SpawnRightSideP1();
                currentP1TimeRight = 0;
            }
        }

        //SPAWN P2'S RIGHT SIDE
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (currentP2TimeRight > cd)
            {
                GameObject agent = Instantiate(agentPF, p2spawnPoint.transform.position, Quaternion.identity);
                PathFollowerHAR agentSc = agent.GetComponent<PathFollowerHAR>();
                agentSc.SpawnRightSideP2();
                currentP2TimeRight = 0;
            }
        }

        //SPAWN P2'S LEFT SIDE
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (currentP2TimeLeft > cd)
            {
                GameObject agent = Instantiate(agentPF, p2spawnPoint.transform.position, Quaternion.identity);
                PathFollowerHAR agentSc = agent.GetComponent<PathFollowerHAR>();
                agentSc.SpawnLeftSideP2();
                currentP2TimeLeft = 0;
            }
        }
    }
}
