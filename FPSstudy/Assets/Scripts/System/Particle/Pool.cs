using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    // 初期プール数
    [SerializeField, Tooltip("初期プール数")]
    int _firstCnt = 10;
    // オブジェクトプールリスト 
    static public List<GameObject> _poolList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Init()
    {
        //var obj = new GameObject();
        //obj.name = "PoolList";
        for (int i = 0; i < _firstCnt; i++)
        {
            var pool = new GameObject();
            pool.name = "PoolObj";
            //pool.transform.parent = obj.transform;
            pool.SetActive(false);
            _poolList.Add(pool);
        }
    }

    public GameObject CheckPool(GameObject prefab)
    {
        foreach(var obj in _poolList)
        {
            if(obj.activeInHierarchy == false)
            {
                obj.SetActive(true);
                obj.name = prefab.name;
                return obj;
            }
        }

        var newObj = CreateObj(prefab);
        newObj.name = prefab.name;
        _poolList.Add(newObj);

        return newObj;
    } 

    private GameObject CreateObj(GameObject prefab)
    {
        GameObject obj = new GameObject();
        obj.SetActive(true);

        return obj;
    }

}
