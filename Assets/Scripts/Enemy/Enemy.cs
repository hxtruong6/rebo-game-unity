﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Slug,
    Solider
}

public class Enemy : ReboObject
{
    public float attackMoveSpeed = 150;
    public float attackRate = 2;
    public float attackRange = 2;
    public float spottOutRange = 3;
    public float maxTimePerMoving = 5;
    public float maxTimePerIdle = 2;
    public float maxTimeBetween2TakeDamage = 5;

    public Level level;
    public HealthBar health;

    // Range move
    public Vector2 leftRangeMove;
    public Vector2 rightRangeMove;

    protected Transform player;
    protected Vector2 originalPos;
    public EnemyType type = EnemyType.Slug;
    protected float timeCountToAttack = 0;
    protected float timeCountToRecuperate = 0;

    protected EnemyAutoControl autoControl;

    private AudioSource audioSource;


    protected override void Setup()
    {
        health.currentHealth = health.maxHealth = maxHealth;

        damage = new Damage(baseDamage, attackDamage);
        level = new Level();
        vision = new Vision(transform, spottOutRange, attackRange, leftRangeMove, rightRangeMove);

        player = GameObject.FindGameObjectWithTag("Player").transform;
        characterSprite = GetComponent<SpriteRenderer>();

        originalPos = transform.position;

        autoControl = new EnemyAutoControl(this, player, maxTimePerMoving, maxTimePerIdle);
        timeCountToRecuperate = maxTimeBetween2TakeDamage;
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {   
        autoControl.Execute();

        if (timeCountToRecuperate > maxTimeBetween2TakeDamage)
        {
            if (health.gameObject.activeSelf)
                health.gameObject.SetActive(false);
            health.currentHealth = health.maxHealth;
        }

        timeCountToAttack += Time.deltaTime;
        timeCountToRecuperate += Time.deltaTime;
    }

    public virtual void MoveToLef(bool left)
    { 
        SetRun_Animation(true);
        MoveToLeft(left, runSpeed);
    }

    public virtual void ChaseToLef(bool left)
    {
        SetRun_Animation(true);
        MoveToLeft(left, 1.5f * runSpeed);
    }

    protected virtual Vector2 GetPushForceWhenAttacking()
    {
        float pushForce = 0.8f * attackMoveSpeed;
        if (LookToTheLeft())
            pushForce = -pushForce;
        return new Vector2(pushForce, 0);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                if (health.IsAlive())
                {
                    collision.gameObject.GetComponent<Character>().TakeDamage(GetAttackDamage());

                    collision.gameObject.GetComponent<Character>().Move(GetPushForceWhenAttacking());
                }

                break;
        }
    }

    public virtual void Chase()
    {
        SetRun_Animation(true);
        ChaseToLef(vision.ShouldGoLeftToAttack(player.position));
    }

    public virtual IEnumerator DestroyEnemy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().updateNumberOfEnemiesAnnihilated(type);

        Destroy(gameObject);
    }
    //-----------------------------------------------
    public override void Idle()
    {
        SetRun_Animation(false);
    }    

    protected override bool CanJump()
    {
        return true;
    }

    protected override void Jump(Vector2 force)
    {
        Move(force);
    }

    public override void MoveToLeft(bool left, float force)
    {
        base.MoveToLeft(left, force);
    } 

    protected override void WillTakeDamage(float damage)
    {
        
        timeCountToRecuperate = 0;
        if (!health.gameObject.activeSelf)
        {
            health.gameObject.SetActive(true);
        }
    }

    protected override void TakingDamage(float damage)
    {
        SetTakeDamage_Animation();
        health.TakeDamage(damage);
    }

    protected override void DidTakedDamage(float damage)
    {
        FindObjectOfType<SoundManager>().PlayEnemySound(audioSource, SoundType.BeAttacked, type);
        if (!isAlive())
        {
            Die();
        }
    }

    public override bool CanAttack()
    {
        return (timeCountToAttack >= attackRate && vision.CanAttack(player.position));
    }

    protected override void WillAttack()
    {
        SetAttack_Animation();
        timeCountToAttack = 0;
    }

    protected override void Attacking()
    {
        MoveToLeft(LookToTheLeft(), attackMoveSpeed);
    }

    protected override void DidAttacked()
    {
        //MoveToLeft(!LookToTheLeft(), runSpeed);
    }

    public override float GetAttackDamage()
    {
        return damage.GetAttackDamage() + level.GetAdditionDamage();
    }

    public override float GetBaseDamage()
    {
        return damage.GetBaseDamage() + level.GetAdditionDamage();
    }

    public override bool isAlive()
    {
        return health.IsAlive();
    }

    protected override void WillBeDied()
    {
        FindObjectOfType<SoundManager>().PlayEnemySound(audioSource, SoundType.Die, type);
        SetDie_Animation();
        health.gameObject.SetActive(false);
    }

    protected override void DidDied()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        StartCoroutine(DestroyEnemy(1.5f));
    }
    //--------------------------------------------
    // Helper Method
    private void SetRun_Animation(bool value)
    {
        GetComponent<Animator>().SetBool(AnimationConstants.ENEMY_MOVING, value);
    }

    private void SetDie_Animation()
    {
        GetComponent<Animator>().SetTrigger(AnimationConstants.ENEMY_DYING);
    }


    private void SetAttack_Animation()
    {
        GetComponent<Animator>().SetTrigger(AnimationConstants.ENEMY_ATTACKING);
    }

    private void SetTakeDamage_Animation()
    {
        GetComponent<Animator>().SetTrigger(AnimationConstants.ENEMY_TAKING_DAMAGE);
    }

}
