using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMng : MonoBehaviour
{
    private float hSpeed = 2.0f;
    private float vSpeed = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
  [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.transform.position.x,
     player.transform.position.y +1.5f,
     player.transform.position.z+0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        yaw += hSpeed * Input.GetAxis("Mouse X");
        pitch -= vSpeed * Input.GetAxis("Mouse Y");
        transform.position = new Vector3(player.transform.position.x, 
            player.transform.position.y +1.5f, 
            player.transform.position.z+0.2f);
        transform.eulerAngles= new Vector3(pitch, yaw, 0);
        
    }

}
