using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // 初期座標
    private Vector3 _fristPos;
    [SerializeField]
    private float _frameSpeed = 0.05f;
    [SerializeField, Tooltip("上下の移動距離")]
    private float _distance = 1f;
    // Start is called before the first frame update
    void Start()
    {
        _fristPos = transform.position;
    }

    // 浮遊処理
    private void Floating()
    {
        transform.position = new Vector3(_fristPos.x, _fristPos.y + Mathf.Sin(Time.frameCount * _frameSpeed) * _distance, _fristPos.z);
    }

    // 回転処理
    private void Rotaion()
    {
        transform.Rotate(new Vector3(0, 1, 0));
    }

    // Update is called once per frame
    void Update()
    {
        Floating();
        Rotaion();
    }

    private void OnTriggerStay(Collider other)
    {
        Destroy(gameObject);
    }
}
