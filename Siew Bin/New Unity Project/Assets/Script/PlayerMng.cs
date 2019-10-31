using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMng : MonoBehaviour
{
    float distToGround;
    public new CameraMng camera;
    Rigidbody rb;
    bool IsMoving = false;
    [SerializeField]
    private LineRenderer lineR;
    private RaycastHit sightLine;
    

    // Start is called before the first frame update
    void Start()
    {
        lineR = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        IsMoving = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (camera.GetFPSflag())
        {
            FPSMovement();
        }
        else
        {
            TPSMovement();
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
        if (Input.GetKeyDown("p"))
        {
            GetComponent<Animator>().SetTrigger("Damaged");
        }

    }

    void FPSMovement()
    {
        if (Input.GetKey("d"))
        {
            SetSpeed(1.0f);
            transform.localPosition += transform.TransformDirection(0.1f, 0.0f, 0.0f);
            IsMoving = true;
        }
        if (Input.GetKey("a"))
        {
            SetSpeed(1.0f);
            transform.localPosition += transform.TransformDirection(-0.1f, 0.0f, 0.0f);
            IsMoving = true;
        }
        if (Input.GetKey("w"))
        {
            SetSpeed(1.0f);
            transform.localPosition += transform.TransformDirection(0.0f, 0.0f, 0.1f);
            IsMoving = true;

        }
        if (Input.GetKey("s"))
        {
            SetSpeed(1.0f);
            transform.localPosition += transform.TransformDirection(0.0f, 0.0f, -0.1f);
            IsMoving = true;
        }
       
        if (!IsMoving)
        {
            SetSpeed(0.0f);
        }
    }

    void TPSMovement()
    {
        if (Input.GetKey("d"))
        {
            SetSpeed(1.0f);
            Vector3 cameraAngle = new Vector3(transform.localEulerAngles.x,
                                 camera.transform.localEulerAngles.y + 90f, transform.localEulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(cameraAngle), 0.15f);
            IsMoving = true;
        }
        if (Input.GetKey("a"))
        {
            SetSpeed(1.0f);
            Vector3 cameraAngle = new Vector3(transform.localEulerAngles.x,
                             camera.transform.localEulerAngles.y - 90f, transform.localEulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(cameraAngle), 0.15f);
            IsMoving = true;
        }
        if (Input.GetKey("w"))
        {
            SetSpeed(1.0f);
            Vector3 cameraAngle = new Vector3(transform.localEulerAngles.x,
                             camera.transform.localEulerAngles.y, transform.localEulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(cameraAngle), 0.15f);
            IsMoving = true;

        }
        if (Input.GetKey("s"))
        {
            SetSpeed(1.0f);

            Vector3 cameraAngle = new Vector3(transform.localEulerAngles.x,
                             camera.transform.localEulerAngles.y - 180f, transform.localEulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(cameraAngle), 0.15f);

            IsMoving = true;

        }
        if (!IsMoving)
        {
            SetSpeed(0.0f);
        }
        else
        {
            transform.localPosition += transform.TransformDirection(0.0f, 0.0f, 0.1f);
        }
        //lineR.SetPosition(0, transform.position);
        //if(Physics.Raycast(transform.position,transform.forward,out sightLine,Mathf.Infinity))
        //{
        //    lineR.SetPosition(1, sightLine.point);
        //}
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
