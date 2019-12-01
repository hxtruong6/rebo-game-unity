using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Object
{
    public float baseDamage, attackDamage;

    public Damage(float baseDamage = 0, float acttackDamage = 0)
    {
        this.baseDamage = baseDamage;
        this.attackDamage = acttackDamage;
    }

    public float GetBaseDamage()
    {
        return baseDamage;
    }

    public float GetAttackDamage()
    {
        return attackDamage;
    }

    public float GetComboDamage()
    {
        return GetBaseDamage() + GetAttackDamage();
    }
}
