using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowerWeaponHAR : MonoBehaviour
{
    public bool c_player1 = false;
    public bool c_player2 = false;
    public List<Collider> collidersInRange = new List<Collider>();
    int dmg = 30;
    int dmgToTower = 10;

    void Start()
    {
        PathFollowerHAR parentTeam = GetComponentInParent<PathFollowerHAR>();
        if(parentTeam.player1 == true)
        {
            c_player1 = true;
        }
        if(parentTeam.player2 == true)
        {
            c_player2 = true;
        }
    }

    void Update()
    {
        collidersInRange.RemoveAll(collider => collider == null);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player2" && c_player1 == true)
        {
            collidersInRange.Add(other);
            AgentBasicHAR target = other.GetComponent<AgentBasicHAR>();
            TowerPoorLogicHAR tower = other.GetComponent<TowerPoorLogicHAR>();
            if (target)
            {
                target.TakeDamage(dmg);
            }
            if (tower)
            {
                tower.TakeDamage(dmgToTower);
            }

        }
        if (other.tag == "Player1" && c_player2 == true)
        {
            collidersInRange.Add(other);
            AgentBasicHAR target = other.GetComponent<AgentBasicHAR>();
            TowerPoorLogicHAR tower = other.GetComponent<TowerPoorLogicHAR>();
            if (target)
            {
                target.TakeDamage(dmg);
            }
            if (tower)
            {
                tower.TakeDamage(dmgToTower);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player1" || other.tag == "Player2")
        {
            collidersInRange.Remove(other);
        }
    }
}
