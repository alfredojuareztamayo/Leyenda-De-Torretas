using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ArrowTower : MonoBehaviour
{
    private float lifeTime = 2f;
    private float maxVel = 5f;
    private float steeringBehaviour = 5f;
    private float maxSpeed = 1f;

    private void Start()
    {
        //Destroy(gameObject,lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAl"))
        {
            Destroy(gameObject);
        }

    }

    public float MaxVel()
    {
        return maxVel;
    }
    public float SteeringBehaviour()
    {
        return steeringBehaviour;
    }
    public float MaxSpeed()
    {
        return maxSpeed;
    }
}
