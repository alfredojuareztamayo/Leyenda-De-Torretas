using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowWeapon : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform originPoint;
    [SerializeField] private bool isShooting;
    [SerializeField] private float coldDown;
    private GameObject arrow;
    private void Start()
    {
        coldDown = 0.5f;
        isShooting = false;
    }
    public void Fire()
    {
        if (!isShooting)
        {
            StartCoroutine(FireArrow());
            isShooting = true;
         
        }
        //StartCoroutine(WaitForAnimation());
    }

    private IEnumerator FireArrow()
    {
        Instantiate(arrowPrefab, originPoint.position, originPoint.rotation);
        yield return new WaitForSeconds(coldDown);
        isShooting = false;
    }
    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1f);
        

    }
}
