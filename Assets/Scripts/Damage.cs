using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Object
{
    public float baseDamage, currentDamage;

    public Damage()
    {
        baseDamage = 50;
        currentDamage = baseDamage;
    }

    public float GetCurrentDamage()
    {
        return currentDamage;
    }
}
