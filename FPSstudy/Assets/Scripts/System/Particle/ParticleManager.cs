using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : Pool
{
    [HideInInspector]
    public ParticleManager instance;

    public ParticleManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = "ParticleManager";
                instance = obj.AddComponent<ParticleManager>();
            }
            return instance;
        }
    }
    // Instance
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject Create()
    {
        return null;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
