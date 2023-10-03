using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBulletHAR : AgentBasicHAR
{
    public bool c_player1 = false;
    public bool c_player2 = false;
    int dmg = 100;
    Vector3 currentVel = Vector3.zero;
    public Transform target;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            rb.velocity = SteeringBehavioursHAR.seek(transform, target.position);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform n_target)
    {
        target = n_target;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player2" && c_player1 == true)
        {
            AgentBasicHAR target = other.GetComponent<AgentBasicHAR>();

            if (target)
            {
                target.TakeDamage(dmg);
            }
            Destroy(gameObject);
        }
        if (other.tag == "Player1" && c_player2 == true)
        {
            AgentBasicHAR target = other.GetComponent<AgentBasicHAR>();

            if (target)
            {
                target.TakeDamage(dmg);
            }
            Destroy(gameObject);
        }
    }
}
