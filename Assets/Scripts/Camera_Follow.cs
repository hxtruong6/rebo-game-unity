using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : ReboRootObject
{
    public Transform target;

    public float smoothSpeed = 0.15f;

    public Vector2 leftRange;
    public Vector2 rightRange;

    protected GameObject[] arrayGameObject;

    public override void SetupInAwake()
    {
        base.SetupInAwake();
        QualitySettings.vSyncCount = 0;

        arrayGameObject = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void Update()
    {
        for (int i = 0; i < arrayGameObject.Length; i++)
            if (arrayGameObject[i] != null)
            {
                if (Vector2.Distance(target.position, arrayGameObject[i].transform.position) <= 30)
                {
                    arrayGameObject[i].gameObject.SetActive(true);
                }
                else
                {
                    arrayGameObject[i].gameObject.SetActive(false);
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
