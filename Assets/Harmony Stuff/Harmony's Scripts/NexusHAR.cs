using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusHAR : MonoBehaviour
{
    public List<GameObject> towers = new List<GameObject>();
    int hp = 700;
    bool vulnerable;

    void Start()
    {
        vulnerable = false;
    }

    void Update()
    {
        if(towers.Count == 0)
        {
            vulnerable = true;
        }
        towers.RemoveAll(gameObject => gameObject == null);
        Destroy();
    }

    public void TakeDamage(int dmg)
    {
        if(vulnerable == true)
        {
            hp -= dmg;
            return;
        }
        Debug.Log("No damage received.");
    }

    void Destroy()
    {
        if(hp <= 0)
        {
            Destroy(gameObject, 1);
        }
    }
}
