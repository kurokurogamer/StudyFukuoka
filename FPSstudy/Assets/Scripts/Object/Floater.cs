using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField]
    private float _degreesPerSecond = 15.0f;
    [SerializeField]
    private float _amplitude = 0.015f;
    [SerializeField]
    private float _frequency = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * _degreesPerSecond, 0f);

        Vector3 newPos = transform.position;
        newPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * _frequency) * _amplitude;

        transform.position = newPos;
    }
}
