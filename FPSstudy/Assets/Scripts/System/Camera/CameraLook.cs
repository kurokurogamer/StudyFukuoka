using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField, Tooltip("ターゲット")]
    private Transform target;
    [SerializeField]
    private float smoothSpeed = 0.125f;
    [SerializeField]
    private Vector3 offset;


    // Update is called once per frame
    void Update()
    {
        //transform.position = target.position + offset;
        transform.LookAt(new Vector3(target.position.x, target.position.y, target.position.z));
    }
}
