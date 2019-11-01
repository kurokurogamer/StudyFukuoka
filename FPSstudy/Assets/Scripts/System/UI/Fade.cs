using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
    private enum FADE_TYPE
    {
        IN,     // フェイドイン
        OUT,    // フェイドアウト
        LOOP,   // フェイドループ
        MAX
    };
    // フェイド用イメージ保存変数
    private Image _image;
    private Color _color;
    // RGB値
    float r, g, b;
    // アルファ値(0から1まで)
    float _alpha;

    // フェイド速度(フレーム単位)
    [SerializeField, Tooltip("フェイド速度(1フレーム単位)")]
    private float _FadeFlamTime = 5;
    // フェイドフラグtrueの状態がFadeOut,falseがFadeIn
    [SerializeField, Tooltip("フェイドの状態フラグ")]
    private bool _isFade;
    private bool _isNext;
    // Start is called before the first frame update
    void Start()
    {
        _isFade = true;                     // 初期状態設定(FadeIn)
        _image  = GetComponent<Image>();    // フェイドするイメージの取得
        _color  = _image.color;             // 初期色を取得
        _alpha  = 1;                        // 透明度指定(不透明)
        _isNext = false;
    }

    private bool FadeIn()
    {
        Debug.Log("フェイドインスタート");
        bool retFlag = false;
        _alpha -= Time.deltaTime;
        _image.enabled = true;
        SetAlpha();
        // 透明度が0以下なら
        if (_alpha <= 0)
        {
            _alpha = 0;                 // 透明度を0に設定
            _image.enabled = false;     // キャンバスを非有効化
            StopAllCoroutines();        // 全てのコルーチンをストップ
            retFlag = true;             // フェイド完了を意味するtrueに
        }
        return retFlag;
    }

    private bool FadeOut()
    {
        Debug.Log("アウトループ");
        _alpha += Time.deltaTime;       // alpha値を加算
        // アルファ値が不透明の255を超えているなら
        if (_alpha >= 1)
        {
            _alpha = 1;
            StopAllCoroutines();
            if (_isNext)
            {
                NextScene();
            }
        }
        return false;
    }

    public void NextScene()
    {
        SceneCtl.Instance.NextScene();
    }

    private void SetAlpha()
    {
        _image.color = new Color(_color.r, _color.g, _color.b,_alpha);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _isNext = true;
            StartCoroutine(FadeStart());
        }
    }

    IEnumerator FadeStart()
    {
        Debug.Log("コルーチンの開始");
        _image.enabled = true;                      // イメージの有効化
        _isFade = _isFade == true ? false : true;   // フェイドの状態を変更

        // ループを開始する
        while (true)
        {
            if (_isFade)
            {
                FadeOut();  // フェイドアウト
            }
            else
            {
                FadeIn();   // フェイドイン
            }
            // alpha値を反映させる
            SetAlpha();
            yield return null;
        }
    }
}
