using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.15f;

    public Vector2 leftRange;
    public Vector2 rightRange;

    private void Awake()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 45;
#endif
    }

    void LateUpdate()
    {
        Vector3 temp = transform.position;
        temp.x += (target.position.x - transform.position.x) * smoothSpeed;

        if (temp.x >= leftRange.x && temp.x <= rightRange.x)
        {
            transform.position = temp;
        }
    }


}
