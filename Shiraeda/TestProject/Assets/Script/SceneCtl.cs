using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtl : MonoBehaviour
{
    public enum SCENE
    {
        TITEL,
        GAME,
        RESULT,
        SAMPLE,
        MAX
    }
    private static SceneCtl instance = null;
    string _sceneName;
    public static SceneCtl Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = "AudioManager";
                instance = obj.AddComponent<SceneCtl>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        // アクティブなシーンの名前を取得する
        _sceneName = SceneManager.GetActiveScene().name;
    }

    public void MoveScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MoveScene(SCENE type)
    {
        switch (type)
        {
            case SCENE.TITEL:
                _sceneName = "TitleScene";
                break;
            case SCENE.GAME:
                _sceneName = "GameScene";
                break;
            case SCENE.RESULT:
                _sceneName = "ResultScene";
                break;
            case SCENE.SAMPLE:
                _sceneName = "SampleScene";
                break;
            case SCENE.MAX:
            default:
                _sceneName = "";
                break;
        }
        SceneManager.LoadScene(_sceneName);
        /// 使い方
        ///SceneCtl.Instance.MoveScene(SceneCtl.SCENE.GAME);
    }

    private void CheckScene()
    {
        // ActiveなSceneの名前を取得
        _sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
