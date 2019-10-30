using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;

public class Effect : Pool
{
    // パーティクルのリスト
    List<ParticleSystem> _particlesList = new List<ParticleSystem>();
    // 生成するパーティクルのプレハブリスト
    [SerializeField, Tooltip("生成するパーティクルのプレハブのリスト")]
    List<GameObject> _CreateList = new List<GameObject>();
    // 生成したパーティクルの親子関係の設定フラグ
    [SerializeField, Tooltip("子オブジェクトにするか")]
    bool _childFlag = false;
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
        if (id >= _particlesList.Count)
        {
            return;
        }
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
        if (id >= _particlesList.Count)
        {
            return;
        }
        // 指定されたパーティクルを停止
        _particlesList[id].Stop();
    }

    // パーティクルを生成(通常呼び出し)
    public void Create(int id, bool loop = false)
    {
        if (id >= _CreateList.Count)
        {
            return;
        }
        // 自身の座標にパーティクルを生成する
        Create(id, transform.position, _CreateList[id].transform.rotation, loop);
    }
    // パーティクルを生成(座標指定)
    public void Create(int id, Vector3 pos, bool loop = false)
    {
        if (id >= _CreateList.Count)
        {
            return;
        }
        // 指定された座標にパーティクルを生成する
        Create(id, pos, _CreateList[id].transform.rotation, loop);
    }
    // パーティクルを生成(座標・回転角度指定)
    public void Create(int id, Vector3 pos, Quaternion rot, bool loop = false, bool autoStop = false)
    {
        if (id >= _CreateList.Count)
        {
            return;
        }
        // エフェクトを生成
        GameObject effect = Instantiate(_CreateList[id], pos, rot);
        effect.name = _CreateList[id].name;
        // エフェクトにあるパーティクルシステムを取得
        ParticleSystem particle = effect.GetComponent<ParticleSystem>();
        // 別のプレハブを間違って生成してないなら
        if (particle)
        {
            // 直接値が変えられないので別の変数に入れる
            var main = particle.main;
            // ループの設定を変更する
            main.loop = loop;
            if (autoStop)
            {
                // パーティクルの再生時間で停止をかける
                StartCoroutine(StopEffect(main.duration, particle));
            }

        }
        // 子オブジェクトにするかどうか
        if(_childFlag)
        {
            effect.transform.parent = transform;
        }
    }
    IEnumerator StopEffect(float secondTime, ParticleSystem particle)
    {

        yield return null;
    }

    // プールされたオブジェクトからパーティクルを生成
    public GameObject CreatePool(int id, bool loop = false)
    {
        return CreatePool(id, transform.position, _CreateList[id].transform.rotation, loop);
    }
    // プールオブジェクトからパーティクルを生成
    public GameObject CreatePool(int id, Vector3 pos, bool loop = false)
    {
        if (id >= _CreateList.Count)
        {
            return null;
        }
        return CreatePool(id, pos, _CreateList[id].transform.rotation, loop);
    }

    public GameObject CreatePool(int id, Vector3 pos, Quaternion rot, bool loop = false)
    {
        if(id >= _CreateList.Count)
        {
            return null;
        }
        // エフェクトを生成
        GameObject effect = CheckPool(_CreateList[id]);
        // エフェクトにあるパーティクルシステムを取得
        ParticleSystem particle = effect.GetComponent<ParticleSystem>();
        // 別のプレハブを間違って生成してないなら
        if (particle)
        {
            // 直接値が変えられないので別の変数に入れる
            var main = particle.main;
            // メインからループにアクセスし設定する
            main.loop = loop;
        }
        else
        {
            var componets = _CreateList[id].GetComponents<Component>();
            // テストで試作中
            // コンポーネントをコピーした後張り付ける
            foreach (var componet in componets)
            {
                ComponentUtility.CopyComponent(componet);
                ComponentUtility.PasteComponentAsNew(effect);
            }
            particle = effect.GetComponent<ParticleSystem>();
            var main = particle.main;
            main.loop = loop;
        }
        // 子オブジェクトにするかどうか
        if (_childFlag)
        {
            effect.transform.parent = transform;
        }

        return effect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
