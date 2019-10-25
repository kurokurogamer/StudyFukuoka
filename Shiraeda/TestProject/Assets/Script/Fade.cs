using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
    // フェイド用イメージ保存変数
    Image _image;
    Color _color;
    // RGB値
    float r, g, b;
    // アルファ値
    float _alpha;

    // フェイド速度(1秒単位)
    [SerializeField, Tooltip("フェイド速度(1秒単位)")]
    float _FadeFlamTime = 5;
    // テストフラグ
    bool _isFade = false;
    // Start is called before the first frame update
    void Start()
    {
        // フェイド用のイメージを取得
        _image = GetComponent<Image>();
        // 初期色を黒に指定
        _color = _image.color;
        // 透明度指定(不透明)
        _alpha = 1;
    }

    public void FadeIn()
    {
        _alpha -= Time.deltaTime;
        SetAlpha();
        // 透明度が0以下なら
        if (_alpha <= 0)
        {
            _image.enabled = false;
            _alpha = 0;
            _isFade = true;
        }
    }

    public void FadeOut()
    {
        _alpha += Time.deltaTime;
        _image.enabled = true;
        // アルファ値が不透明の255を超えているなら
        if (_alpha >= 1)
        {
            _alpha = 1;
            _isFade = false;
        }
    }

    private void SetAlpha()
    {
        _image.color = new Color(_color.r, _color.g, _color.b,_alpha);
    }

    private void StartFade()
    {
        if(_isFade)
        {
            FadeOut();
        }
        else
        {
            FadeIn();
        }
        SetAlpha();
    }

    // Update is called once per frame
    void Update()
    {
        StartFade();
    }
}
