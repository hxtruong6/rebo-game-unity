using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamage : Object
{
    public float baseDamage, currentDamage;

    public BaseDamage(float baseDamage)
    {
        this.baseDamage = baseDamage;

        currentDamage = baseDamage;
    }

    public float GetCurrentDamage()
    {
        return currentDamage;
    }
}
