using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


public class ArrowStatics : MonoBehaviour
{
    Rigidbody rbArrow;
    float speed = 2f;

    private void Start() {
        rbArrow = GetComponent<Rigidbody>();
        rbArrow.velocity = speed * Vector3.forward;
    }
    private void Update() {
      
    }
    public float Speed {
        get { return speed; }
        set { speed = value; }
    }
}
