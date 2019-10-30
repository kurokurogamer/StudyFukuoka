using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerTransform;
    public Transform[] spawnPoints;

    [SerializeField]
    private GameObject _enemyContainer;

    public float spawnInterval = 5;     // 生成間隔 ( 秒 )
    private float nextSpawnTime = 0;    // 次の生成時間を格納する変数

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnInterval;

            // 敵の生成
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject newEnemy = Instantiate(enemyPrefab, randomPoint.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            Enemy npc = newEnemy.GetComponent<Enemy>();
            npc.playerTransform = playerTransform;
            npc.es = this;
        }
    }
}
