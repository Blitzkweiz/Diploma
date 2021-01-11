using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float heigh;
    void Update()
    {
        transform.position = target.position + new Vector3(0.0f, 0.0f, -heigh);
        transform.LookAt(target.position);
    }
}
