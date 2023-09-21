using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowHAR : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float distance;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y + distance, target.position.z);
    }
}
