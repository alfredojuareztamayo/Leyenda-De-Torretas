using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoorLogicHAR : MonoBehaviour
{
    bool player1 = false;
    bool player2 = false;
    int dmg = 5;
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
        Destroyed();
    }

    void Destroyed()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int dmg)
    {
        hp -= dmg;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player2" && player1 == true)
        {
            AgentBasicHAR target = other.GetComponent<AgentBasicHAR>();
            target.TakeDamage(dmg);
        }
        if (other.tag == "Player1" && player2 == true)
        {
            AgentBasicHAR target = other.GetComponent<AgentBasicHAR>();
            target.TakeDamage(dmg);
        }
    }
}
