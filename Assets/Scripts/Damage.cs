using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Object
{
    public float baseDamage, currentDamage;

    Damage()
    {
        baseDamage = 50;
        currentDamage = baseDamage;
    }

    public float Attack()
    {
        return currentDamage;
    }

    public void LevelUp(float damage)
    {
        if (damage > 0)
        {
            currentDamage += damage;
        }
    }

    public void LevelDown(float damage)
    {
        if (damage > 0)
        {
            currentDamage -= damage;
        }
    }
}
