using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slug : Enemy
{
    void Start()
    {
        runSpeed = 5f;
        jumpSpeed = 250f;
        attackSpeed = 170f;

        timeCountToAttack = 0;
        attackRate = 2f;

        damage = new Damage(100, 200);
        level = new Level();
        vision = new Vision(transform, 3.5f, 2.5f, leftRangeMove, rightRangeMove);

        player = GameObject.FindGameObjectWithTag("Player").transform;
        characterSprite = GetComponent<SpriteRenderer>();

        originalPos = transform.position;

        autoControl = new EnemyAutoControl(this, player, 6, 2);
        health.TakeDamage(990);
    }

    protected override void LookToTheLeft(bool toLeft)
    {
        characterSprite.flipX = !toLeft;
    }

    public override bool LookToTheLeft()
    {
        return !characterSprite.flipX;
    }


}

