using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEffect : MonoBehaviour
{
    // パーティクルのリスト
    List<ParticleSystem> _particlesList = new List<ParticleSystem>();
    // 生成するパーティクルのプレハブリスト
    [SerializeField, Tooltip("生成するパーティクルのプレハブのリスト")]
    List<GameObject> _CreateList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        // 子オブジェクトのパーティクルシステムを取得
        foreach(Transform child in transform)
        {
            ParticleSystem particle = child.GetComponent<ParticleSystem>();
            if(particle)
            {
                _particlesList.Add(particle);
            }
        }
    }

    public void AllPlay()
    {
        // Listに登録されている数分ループさせる
        for (int i = 0; i < _particlesList.Count; i++)
        {
            Play(i);
        }
    }

    public void Play(int id)
    {
        // 指定されたパーティクルを再生
        _particlesList[id].Play();
    }

    public void AllStop()
    {
        // Listに登録されている数分ループさせる
        for (int i = 0; i < _particlesList.Count; i++)
        {
            Stop(i);
        }
    }

    public void Stop(int id)
    {
        // 指定されたパーティクルを停止
        _particlesList[id].Stop();
    }

    // パーティクルを生成(通常呼び出し)
    public void Create(int id, bool loop = false)
    {
        // 自身の座標にパーティクルを生成する
        Create(id, transform.position, transform.rotation, loop);
    }
    // パーティクルを生成(座標指定)
    public void Create(int id, Vector3 pos, bool loop)
    {
        // 指定された座標にパーティクルを生成する
        Create(id, pos, transform.rotation, loop);
    }
    // パーティクルを生成(座標・回転角度指定)
    public void Create(int id, Vector3 pos, Quaternion rot, bool loop)
    {
        // エフェクトを生成
        GameObject effect = Instantiate(_CreateList[id], pos, rot);
        // エフェクトにあるパーティクルシステムを取得
        ParticleSystem particle = effect.GetComponent<ParticleSystem>();
        // 別のプレハブを間違って生成してないなら
        if(particle)
        {
            // 直接値が変えられないので別の変数に入れる
            var main = particle.main;
            // メインからループにアクセスし設定する
            main.loop = loop;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
