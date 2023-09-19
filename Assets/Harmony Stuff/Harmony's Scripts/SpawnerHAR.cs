using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHAR : MonoBehaviour
{
    public GameObject agentPF;
    public GameObject spawnPoint;
    float cd = 1;
    float currentTimeRight;
    float currentTimeLeft;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentTimeLeft += Time.deltaTime;
        currentTimeRight += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentTimeLeft > cd)
            {
                GameObject agent = Instantiate(agentPF, spawnPoint.transform.position, Quaternion.identity);
                PathFollowerHAR agentSc = agent.GetComponent<PathFollowerHAR>();
                agentSc.SpawnLeftSideP1();
                currentTimeLeft = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (currentTimeRight > cd)
            {
                GameObject agent = Instantiate(agentPF, spawnPoint.transform.position, Quaternion.identity);
                PathFollowerHAR agentSc = agent.GetComponent<PathFollowerHAR>();
                agentSc.SpawnRightSideP1();
                currentTimeRight = 0;
            }
        }
    }
}
