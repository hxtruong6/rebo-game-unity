using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : ReboRootObject
{
    public Transform target;

    public float smoothSpeed = 0.15f;

    public Vector2 leftRange;
    public Vector2 rightRange;

    public override void SetupInAwake()
    {
        base.SetupInAwake();
        QualitySettings.vSyncCount = 0;
    }

    private void Update()
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i=0; i<array.Length; i++)
        {
            if (Vector2.Distance(target.position, array[i].transform.position) >= 30)
            {
                array[i].gameObject.SetActive(false);
            }
            else
            {
                array[i].gameObject.SetActive(true);
            }
        }
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
