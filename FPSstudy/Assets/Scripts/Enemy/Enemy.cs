using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float attackDistance;
    public float movementSpeed;
    public float healtPoint;

    // 攻撃用変数
    public float damage = 5.0f;
    public float attackRate = 0.5f;
    public Transform firePoint;

    public Transform playerTransform;
    private NavMeshAgent agent;
    [HideInInspector]
    public EnemySpawner es;
    private Rigidbody rb;
    [SerializeField]
    private GameObject item;
    public GameObject Item
    {
        get { return item; }
        set { item = value; }
    }

    private void OnDestroy()
    {
        if(item != null)
        {
            Instantiate(item, transform.position, transform.rotation);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDistance;
        agent.speed = movementSpeed;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance - attackDistance < 0.01f)
        {
            // 攻撃制御
            RaycastHit hit;
            if(Physics.Raycast(firePoint.position,firePoint.forward,out hit,attackDistance))
            {
                if(hit.transform.CompareTag("Player"))
                {
                    Debug.DrawLine(firePoint.position, firePoint.position + firePoint.forward * attackDistance, Color.cyan);
                }
            }
        }

        // 移動制御
        agent.destination = playerTransform.position;

        // 目標を見る
        transform.LookAt(new Vector3(
            playerTransform.position.x,
            transform.position.y,
            playerTransform.position.z));

        rb.velocity *= 0.99f;
    }
}
