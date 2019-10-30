using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGizmos : MonoBehaviour
{
    enum GIZMOSE_MODE
    {
        CUBE,
        SPHERE,
        MAX
    }

    // デバック表示
    [SerializeField]
    bool drawGizmos = true;
    [SerializeField]
    private GIZMOSE_MODE _mode = GIZMOSE_MODE.CUBE;
    [SerializeField]
    Color color = Color.white;
    [SerializeField]
    float _size = 1;
    [SerializeField]
    LayerMask _layer = 0;

    private void OnDrawGizmos()
    {
        // デバック表示
        if (!drawGizmos)
        {
            return;
        }
        //Gizmos角度変更用
        Matrix4x4 rotationMatrix = transform.localToWorldMatrix;
        Gizmos.matrix = rotationMatrix;
        Gizmos.color = color;
        // デバックモード
        if (_mode == GIZMOSE_MODE.CUBE)
        {
            Gizmos.DrawCube(Vector3.zero, Vector3.one *_size);
        }
        else if (_mode == GIZMOSE_MODE.SPHERE)
        {
            Gizmos.DrawSphere(Vector3.zero, _size / 2);
        }
    }
}
