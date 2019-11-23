using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildObjects : MonoBehaviour
{
    private List<GameObject> objects;

    void Start()
    {
        objects = new List<GameObject>(transform.childCount);
        foreach (Transform child in transform)
        {
            objects.Add(child.gameObject);
        }
    }

    public void SetActive(bool state)
    {
        foreach (var o in objects)
        {
            o.SetActive(state);
        }
    }
}