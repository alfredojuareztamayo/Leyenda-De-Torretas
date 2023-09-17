using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform originPoint;
    [SerializeField] private Rigidbody rbArrow;
    [SerializeField] private float speed;
    [SerializeField] private bool isShooting;
    [SerializeField] private float coldDown;

    private void Start()
    {
        
        speed = 3f;
        coldDown = 2f;
        isShooting = false;
    }
    public void Fire()
    {
        if (!isShooting)
        {
            GameObject arrow = Instantiate(arrowPrefab, originPoint.position, originPoint.rotation);
            rbArrow = GetComponent<Rigidbody>();
            rbArrow.velocity = speed * originPoint.forward;
            isShooting = true;
            StartCoroutine(FireArrow());
        }
        //StartCoroutine(WaitForAnimation());
    }

    private IEnumerator FireArrow()
    {
        yield return new WaitForSeconds(coldDown);
        isShooting = false;
    }
    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1f);
        

    }
}
