using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField, Tooltip("再生する音のリスト")]
    private List<AudioClip> _clipList = new List<AudioClip>();
    public List<AudioClip> ClipList
    {
        get { return _clipList; }
    }
}
