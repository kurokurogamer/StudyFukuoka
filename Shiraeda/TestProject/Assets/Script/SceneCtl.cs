using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCtl : MonoBehaviour
{
    static SceneCtl instance = null;
    public static SceneCtl Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
