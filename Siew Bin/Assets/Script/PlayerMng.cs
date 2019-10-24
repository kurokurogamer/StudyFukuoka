using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMng : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d"))
        {
           
            transform.localPosition += new Vector3(0.1f,0.0f,0.0f);
        }
        if (Input.GetKey("a"))
        {
            transform.localPosition += new Vector3(-0.1f, 0.0f, 0.0f);
        }
        if (Input.GetKey("w"))
        {
            transform.localPosition += new Vector3(0.0f, 0.0f, 0.1f);
        }
        if (Input.GetKey("s"))
        {
            transform.localPosition += new Vector3(0.0f, 0.0f, -0.1f);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            
            rb.AddForce(new Vector3(0.0f, 5.0f, 0.0f),ForceMode.Impulse);

        }
    }
}
