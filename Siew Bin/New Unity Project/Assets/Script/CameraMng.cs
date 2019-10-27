using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMng : MonoBehaviour
{
    private float hSpeed = 2.0f;
    private float vSpeed = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private bool zoomFlag = false;
    private bool lockOnFlag = false;
    private bool FPSFlag = false;
    private Vector3 playerOffset;

    private Vector3 dist;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject scope;
    private GameObject lockedTarget;
    public float damping = 1;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < scope.transform.childCount; i++)
        {
            scope.transform.GetChild(i).gameObject.SetActive(false);
        }
        dist = transform.position - player.transform.position;
        FPSFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerOffset = new Vector3(player.transform.position.x,
            player.transform.position.y + 1.5f,
            player.transform.position.z + 0.2f);
        //FirstPersonView();
        ThirdPersonView();
    }

    private void LateUpdate()
    {
        

    }

    void FirstPersonView()
    {
        this.GetComponent<Camera>().cullingMask = 0xffff0f;

        yaw += hSpeed * Input.GetAxis("Mouse X");
        pitch -= vSpeed * Input.GetAxis("Mouse Y");

        //transform.position = new Vector3(tmpPos.position.x, tmpPos.position.y + 1.5f, tmpPos.position.z);

        transform.position = playerOffset;
        pitch = Mathf.Clamp(pitch, -60f, 60f);


        //transform.RotateAround(player.transform.position, transform.right, pitch);
        //transform.RotateAround(player.transform.position, transform.up, Input.GetAxis("Mouse X") * hSpeed);
    
            transform.eulerAngles = new Vector3(pitch, yaw, 0);
            player.transform.eulerAngles = new Vector3(0, yaw, 0);
        
        if (Input.GetMouseButtonDown(1))
        {
            Zoom();
        }
    }

    void ThirdPersonView()
    {

        this.GetComponent<Camera>().cullingMask = 0xffffff;

        dist =  Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4.0f, Vector3.up) * dist;

        transform.position = player.transform.position + dist;
        transform.LookAt(player.transform);

        CheckWall();

        if (Input.GetKeyDown("r"))
        {
            lockOnFlag = !lockOnFlag;
            LockOn();
            if (!lockOnFlag)
            {
                pitch = transform.eulerAngles.x;
                yaw = transform.eulerAngles.y;
            }
        }
        if (lockOnFlag)
        {
            player.transform.LookAt(lockedTarget.transform);
        }

    }
    void Zoom()
    {
        for (int i= 0;i < scope.transform.childCount; i++)
        {
            scope.transform.GetChild(i).gameObject.SetActive(!scope.transform.GetChild(i).gameObject.activeSelf);
        }
        //scope.SetActive(!scope.activeSelf);
        this.GetComponent<Camera>().fieldOfView += zoomFlag? 55.0f : -55.0f;
        zoomFlag = !zoomFlag;
    }

    public void LockOn()
    {
        lockedTarget = lockOnFlag ? FindClosestEnemy() : null ;
    }
    private GameObject FindClosestEnemy()
    {
        GameObject[] enemys;
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = playerOffset;
        foreach (GameObject enemy in enemys)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                    closest = enemy;
                    distance = curDistance;
            }
           
        }
        return closest;
    }
     public bool GetFPSflag()
    {
        return FPSFlag;
    }

    void CheckWall()
    {
        RaycastHit hit;
        float desiredAngle = player.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        Vector3 desiredPos = playerOffset + dist;

        if (Physics.Linecast(playerOffset, desiredPos,out hit))
        {
            transform.position = new Vector3(hit.point.x + hit.normal.x * 0.5f, transform.position.y, hit.point.z + hit.normal.z * 0.5f);

        }
    }
}
