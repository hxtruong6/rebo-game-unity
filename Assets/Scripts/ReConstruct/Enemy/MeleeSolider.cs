using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSolider : Enemy
{
    void Start()
    {
        runSpeed = 5f;
        jumpSpeed = 250f;
        attackSpeed = 150f;

        timeCountToAttack = 0;
        attackRate = 1.8f;

        health.currentHealth = health.maxHealth = 200;
        damage = new Damage(50, 100);
        level = new Level();
        vision = new Vision(transform, 3f, 2f, leftRangeMove, rightRangeMove);

        player = GameObject.FindGameObjectWithTag("Player").transform;
        characterSprite = GetComponent<SpriteRenderer>();

        originalPos = transform.position;

        autoControl = new EnemyAutoControl(this, player, 6, 2);

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
