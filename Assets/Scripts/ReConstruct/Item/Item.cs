using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ReboRootObject
{
    
    void Start()
    {
        Setup();
    }

    protected virtual void Setup()
    {

    }

    public virtual void Destroy(float waitTime)
    {
        StartCoroutine(DestroyItem(waitTime));
    }

    public virtual IEnumerator DestroyItem(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        Destroy(gameObject);
    }   

    protected virtual void SetGone_Animation(bool value)
    {
        GetComponent<Animator>().SetBool(AnimationConstants.ITEAM_GONE, value);
    }
}
