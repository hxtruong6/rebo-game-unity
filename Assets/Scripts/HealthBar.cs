using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;

    public float maxHealth, currentHealth;

    void Start()
    {
        bar = transform.Find("Bar");
        maxHealth = 1000f;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        ChangeHealth();
    }

    public void BeAttacked(float damage)
    {
        if (damage > 0)
        {
            currentHealth -= damage;
            ChangeHealth();
        }
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
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }else if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        
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


}
