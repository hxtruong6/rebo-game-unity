using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Object 
{
    public float maxHealth;
    public float currentHealth;

    public Health(float maxHealth, float currentHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
    }

    private void ChangeHealth()
    {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        if (currentHealth < 0)
            currentHealth = 0;
    }

    public float TakeDamage(float damage)
    {
        if (damage > 0)
        {
            currentHealth -= damage;
            ChangeHealth();
        }
        return currentHealth;
    }
    
    public float Recuperate(float blood)
    {
        if (blood > 0)
        {
            currentHealth += blood;
            ChangeHealth();
        }
        return currentHealth;
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }
}
