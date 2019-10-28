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
    // アルファ値(0から1まで)
    float _alpha;

    // フェイド速度(1秒単位)
    [SerializeField, Tooltip("フェイド速度(1フレーム単位)")]
    float _FadeFlamTime = 5;
    // フェイドフラグtrueの状態がFadeOut,falseがFadeIn
    [SerializeField, Tooltip("フェイドの状態フラグ : trueでFadeOut, falseでFadeIn")]
    bool _isFade;
    // Start is called before the first frame update
    void Start()
    {
        _isFade = true;
        // フェイド用のイメージを取得
        _image = GetComponent<Image>();
        // 初期色を黒に指定
        _color = _image.color;
        // 透明度指定(不透明)
        _alpha = 1;
    }

    public void FadeIn()
    {
        Debug.Log("フェイドインスタート");
        _alpha -= Time.deltaTime;
        _image.enabled = true;
        SetAlpha();
        // 透明度が0以下なら
        if (_alpha <= 0)
        {
            _image.enabled = false;
            _alpha = 0;
            StopAllCoroutines();
        }
    }

    public void FadeOut()
    {
        Debug.Log("フェイドアウトスタート");
        _alpha += Time.deltaTime;
        _image.enabled = true;
        // アルファ値が不透明の255を超えているなら
        if (_alpha >= 1)
        {
            _alpha = 1;
            StopAllCoroutines();
            SceneCtl.Instance.MoveScene(SceneCtl.SCENE.GAME);
        }
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
            // フェイドの状態を変更(テストコード)
            _isFade = _isFade == true ? false : true;

            Debug.Log("Fキーが押された");
            StartCoroutine(FadeStart());
        }
    }

    IEnumerator FadeStart()
    {
        Debug.Log("コルーチンの開始");

        // 無限ループを開始する
        while (true)
        {
            Debug.Log("コルーチンループ中");
            if (_isFade)
            {
                FadeOut();
            }
            else
            {
                FadeIn();
            }
            // アルファ値を反映させる
            SetAlpha();
            yield return null;
        }
    }
}
