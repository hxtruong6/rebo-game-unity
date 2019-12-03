using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slug : Enemy
{

    protected override void Setup()
    {
        base.Setup();
        type = EnemyType.Slug;
    }

    protected override void LookToTheLeft(bool toLeft)
    {
        characterSprite.flipX = !toLeft;
    }

    public override bool LookToTheLeft()
    {
        return !characterSprite.flipX;
    }

    public override float GetAttackDamage()
    {
        float d = base.GetAttackDamage();
        if (Random.Range(0, 100) % 20 == 0)
            d *= 2;
        return d;
    }
}

