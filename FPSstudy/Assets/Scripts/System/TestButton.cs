using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    private Effect effect;
    private Sound _sound;
    [SerializeField]
    private GameObject obj;
    [SerializeField]
    private ParticleSystem ef;
    // Start is called before the first frame update
    void Start()
    {
        effect = GetComponent<Effect>();
        _sound = GetComponent<Sound>();
        var test1 = Instantiate(obj);
        test1.name = "test1";
        var test2 = Instantiate(ef);
        test2.name = "test2";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // パーティクル生成(コピー)
            effect.Create(0, transform.position);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            // パーティクル生成(使いまわし)
            effect.CreatePool(0, transform.position);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            // パーティクル再生
            effect.Play(0);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            // パーティクル停止
            effect.Stop(0);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            // BGM再生
            AudioManager.Instance.PlayBGM(_sound.ClipList[0]);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            // SE再生
            AudioManager.Instance.PlaySE(_sound.ClipList[1]);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioManager.Instance.PlayShotSE(_sound.ClipList[1]);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // サウンド停止
            AudioManager.Instance.Stop();
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            AudioManager.Instance.Volume(0.5f);
        }
    }
}
