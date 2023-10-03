using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerationTowe : MonoBehaviour
{
    public GameObject CanonIniciar;
    public GameObject Bala;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        DisparaBala();
    }

    void DisparaBala()
    {
        Instantiate(Bala,CanonIniciar.GetComponent<Transform>().position, Quaternion.identity);
    }
}
