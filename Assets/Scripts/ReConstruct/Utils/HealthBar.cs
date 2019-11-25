using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float maxHealth;

    public float currentHealth;

    private Health health;
    private Transform bar;

    void Start()
    {
        currentHealth = maxHealth = 1000;

        bar = transform.Find("Bar");

        SetSize(1.0f);

        health = new Health(1000);
    }

    private void Update()
    {
        ChangeHealth();
    }

    public float TakeDamage(float damage)
    {
        if (damage > 0)
        {
            currentHealth -= damage;
            if (currentHealth < 0)
                currentHealth = 0;

            ChangeHealth();
        }        

        return currentHealth;
    }

    
    public float Recuperate(float blood)
    {
        if (blood > 0)
        {
            currentHealth += blood;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            ChangeHealth();
        }

        return health.currentHealth;
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
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


    private void SetColor(Color color)
    {
        bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }

    
}
