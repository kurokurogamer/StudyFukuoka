using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    // オーディオソース
    [SerializeField]
    private AudioSource _audioSource = null;
    // コルーチン保存
    private Coroutine _coroutine;
    public static AudioManager Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = "AudioManager";
                instance = obj.AddComponent<AudioManager>();
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
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _coroutine = null;
    }
    // BGMの再生
    public bool PlayBGM(AudioClip clip)
    {
        _coroutine = StartCoroutine(SoundLoop(clip));
        return false;
    }

    public bool PlaySE(AudioClip clip)
    {
        if (_audioSource == null)
        {
            return false;
        }

        _audioSource.PlayOneShot(clip);

        return true;
    }

    public bool PlayShotSE(AudioClip clip)
    {
        // オーディオソースがあるかつ、再生中でないなら
        if(_audioSource == null || _audioSource.isPlaying)
        {
            return false;
        }
        Debug.Log("SEを再生します");
        _audioSource.PlayOneShot(clip);
        return true;
    }

    public bool PlayLoopSE(AudioClip clip)
    {
        StartCoroutine(SoundLoop(clip));
        return false;
    }
    // 音の停止
    public bool Stop()
    {
        if (_audioSource == null)
        {
            return false;
        }
        StopCoroutine(_coroutine);
        _audioSource.Stop();
        return false;
    }

    // ループ用コルーチン
    IEnumerator SoundLoop(AudioClip clip)
    {
        while(true)
        {
            Debug.Log("ループ中");
            if(!_audioSource.isPlaying)
            {
                Debug.Log("再生されてないので、再生する");
                _audioSource.PlayOneShot(clip);
            }
            yield return null;
        }
    }
    // 再生を一時停止する
    public bool Pause()
    {
        if (_audioSource == null)
        {
            return false;
        }
        _audioSource.Pause();
        return true;
    }


    // 音量を変更する
    public void Volume(float volume)
    {
        _audioSource.volume = volume;
    }
}
