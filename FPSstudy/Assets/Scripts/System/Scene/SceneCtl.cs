using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtl : MonoBehaviour
{
    public enum SCENE
    {
        TITEL,
        MAP1,
        RESULT,
        MAX
    }
    
    [HideInInspector]
    public static SceneCtl instance = null;
    public static SceneCtl Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = "SceneManager";
                instance = obj.AddComponent<SceneCtl>();
            }
            return instance;
        }
    }
    // nextシーンの列挙型保存変数
    [SerializeField]
    private SCENE _nextScene = SCENE.MAX;
    // シーン名保存変数
    private string _sceneName;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        // アクティブなシーンの名前を取得する
        _sceneName = SceneManager.GetActiveScene().name;
    }

    public void NextScene()
    {
        NextScene(_nextScene);
    }

    public void NextScene(SCENE type)
    {
        // 列挙の中身をみて、指定されたSceneに遷移する
        switch (type)
        {
            case SCENE.TITEL:
                _sceneName = "TitleScene";
                _nextScene = SCENE.MAP1;
                break;
            case SCENE.MAP1:
                _sceneName = "Map_v1";
                _nextScene = SCENE.RESULT;
                break;
            case SCENE.RESULT:
                _sceneName = "ResultScene";
                _nextScene = SCENE.TITEL;
                break;
            case SCENE.MAX:
            default:
                _sceneName = "";
                break;
        }
        // シーンの名前が指定されているなら
        if (_sceneName != "")
        {
            SceneManager.LoadScene(_sceneName);
            Time.timeScale = 0;
        }
    }

    private void GetScene()
    {
        // ActiveなSceneの名前を取得
        _sceneName = SceneManager.GetActiveScene().name;
    }

    public void PauseScene(bool pause)
    {
        GetScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
