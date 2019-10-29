using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.15f;
   
    void LateUpdate()
    {
        Vector3 temp = transform.position;
        temp.x += (target.position.x - transform.position.x) * smoothSpeed;

        if (temp.x >= 0 && temp.x <= 52)
        {
            transform.position = temp;
        }
    }


}
