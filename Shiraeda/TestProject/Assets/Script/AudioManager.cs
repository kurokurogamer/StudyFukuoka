using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    [SerializeField, Tooltip("オーディオクリップ")]
    AudioClip _sound;
    [SerializeField, Tooltip("オーディオソース取得")]
    AudioSource _audioSource = null;
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
        
    }
    public bool PlayBGM()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {

        }
        return false;
    }

    public bool PlaySE()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            _audioSource.PlayOneShot(_sound);
        }
        _audioSource.clip = _sound;
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        PlaySE();
    }
}
