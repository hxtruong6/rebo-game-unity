﻿using System;
using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private Transform bar;

    public float maxHealth;
    public float currentHealth;
   
    private Animator animator;
    public float DestroyDelayTime = 0.5f;

    private Transform[] baries;

    void Start()
    {

        bar = transform.Find("Bar");
       
        currentHealth = maxHealth = 1000;
      
        SetSize(1.0f);
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        ChangeHealth();
    }

    public void BeAttacked(float damage)
    {
        if (damage > 0)
        {
            bool characterDies = (currentHealth - damage) <= 0;
            currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
            ChangeHealth();

            if (characterDies)
            {

         
                StartCoroutine(KillCharacter());

            }
        }
    }

    IEnumerator KillCharacter()
    {
        GetComponent<ChildObjects>().SetActive(false);
        animator.SetTrigger(AnimationName.IS_DYING);
        AnimatorStateInfo currInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(currInfo.length + DestroyDelayTime);
        gameObject.GetComponentInParent<Collider2D>().isTrigger = true;
        Destroy(transform.parent.gameObject);
    }


    public void Recuperate(float blood)
    {
        if (blood > 0)
        {
            currentHealth += blood;
            ChangeHealth();
        }
    }

    private void ChangeHealth()
    {
        float bloodPecent = currentHealth / maxHealth;
        SetSize(bloodPecent);
    }

    private void SetSize(float size)
    {
        bar.localScale = new Vector3(size, 1f);
    }

    internal object Find(string v)
    {
        throw new NotImplementedException();
    }

    private void SetColor(Color color)
    {
        bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }


}