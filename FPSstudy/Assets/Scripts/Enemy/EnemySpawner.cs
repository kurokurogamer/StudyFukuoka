using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private List<GameObject> enemyList = new List<GameObject>();
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField, Tooltip("ドロップアイテム")]
    private GameObject prefab = null;

    [SerializeField]
    private int maxEnemy = 10;          // 最大エネミー数
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private float spawnInterval = 5;    // 生成間隔 ( 秒 )
    private float nextSpawnTime = 0;    // 次の生成時間を格納する変数

    private bool _nextFlag = false;     // 次のSceneへのフラグ

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < maxEnemy; i++)
        {
            CreateEnemy();
            enemyList[i].SetActive(false);
        }
    }

    private void OnDisable()
    {
    }

    // エネミーの生成
    public void CreateEnemy()
    {
        // 敵の生成
        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject newEnemy = Instantiate(enemyPrefab, randomPoint.position, Quaternion.identity);
        // エネミーの格納先コンテナが存在するなら
        if (_enemyContainer != null)
        {
            newEnemy.transform.parent = _enemyContainer.transform;
        }
        Enemy npc = newEnemy.GetComponent<Enemy>();
        npc.playerTransform = playerTransform;
        npc.es = this;
        npc.Item = prefab;
        enemyList.Add(newEnemy);
    }

    public void TimeCreate()
    {   
        // タイムが生成時間を超えているなら
        if (Time.time > nextSpawnTime)
        {
            foreach (var enemy in enemyList)
            {
                // nullチェック
                if(enemy == null)
                {
                    continue;
                }
                // リストから非有効のオブジェクトを探して有効化する
                if (!enemy.activeInHierarchy)
                {
                    // 座標をランダムな位置にする(有効化と非有効化でエネミーを使いまわす場合)
                    //Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                    //enemy.transform.position = randomPoint.position;
                    enemy.SetActive(true);
                    nextSpawnTime = Time.time + spawnInterval;
                    return;
                }
            }
        }
    }

    public void NextCheck()
    {
        _nextFlag = true;
        foreach(var enemy in enemyList)
        {
            if(enemy != null)
            {
                _nextFlag = false;
            }
        }
        if (_nextFlag /*&& Cursor.lockState == CursorLockMode.None*/)
        {
            SceneCtl.Instance.NextScene(SceneCtl.SCENE.RESULT);
        }

        if (_nextFlag)
        {
            //Cursor.lockState = CursorLockMode.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 敵の生成
        TimeCreate();
        // シーン遷移のチェック
        NextCheck();
    }
}
