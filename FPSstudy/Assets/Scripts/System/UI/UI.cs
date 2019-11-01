using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MoveScene()
    {
        Debug.Log("チェック中");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneCtl.Instance.NextScene();
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveScene();
    }
}
