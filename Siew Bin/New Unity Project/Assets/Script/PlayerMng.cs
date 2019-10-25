using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMng : MonoBehaviour
{
    float distToGround;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        bool IsMoving = false;
        if (Input.GetKey("d"))
        {
            SetSpeed(1.0f);
            transform.localPosition += new Vector3(0.1f,0.0f,0.0f);
            IsMoving = true;
        }
        if (Input.GetKey("a"))
        {
            SetSpeed(1.0f);
            transform.localPosition += new Vector3(-0.1f, 0.0f, 0.0f);
            IsMoving = true;
        }
        if (Input.GetKey("w"))
        {
            SetSpeed(1.0f);
            transform.localPosition += new Vector3(0.0f, 0.0f, 0.1f);
            IsMoving = true;

        }
        if (Input.GetKey("s"))
        {
            SetSpeed(1.0f);
            transform.localPosition += new Vector3(0.0f, 0.0f, -0.1f);
            IsMoving = true;

        }
        if (Input.GetKeyDown(KeyCode.Space)&& IsGrounded())
        {
            Debug.Log("Jump");
            //GetComponent<Animator>().SetBool("Jump", true);
            rb.AddForce(new Vector3(0.0f, 5.0f, 0.0f),ForceMode.Impulse);

        }
        if (IsGrounded())
        {
            GetComponent<Animator>().SetTrigger("OnGround");
            GetComponent<Animator>().SetBool("Jump", false);

        }
        else
        {
            GetComponent<Animator>().SetBool("Jump", true);
        }
        if (!IsMoving)
        {
            SetSpeed(0.0f);
        }
    }


    void SetSpeed(float val)
    {
        GetComponent<Animator>().SetFloat("Speed", val);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }


}
