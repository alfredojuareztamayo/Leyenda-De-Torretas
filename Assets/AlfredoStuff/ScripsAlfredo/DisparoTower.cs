using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoTower : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform originOfFire;
    [SerializeField] float speedArrow = 3.5f;
    [SerializeField] float coldDownShoot = 15f;
    private float timeLastShoot;
    public Transform targetArrow;
    private Rigidbody rb;
    private GameObject newArrow;
    private List<GameObject> arrowList = new List<GameObject>();



    private void Update()
    {
       
                
        
    }
    public void ShootArrow()
    {
         targetArrow = GetComponent<TowerPerceptionAndDecision>().target;
        // Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        if (Time.time - timeLastShoot >= coldDownShoot)
        {
            newArrow = Instantiate(arrowPrefab, originOfFire.position, Quaternion.identity);
            rb = newArrow.GetComponent<Rigidbody>();
            timeLastShoot = Time.time;
        }
        if (rb != null)
        {
            rb.velocity = SteeringBHAlfredoArrow.Seek(newArrow.transform, targetArrow.position);
        }
    }
}
