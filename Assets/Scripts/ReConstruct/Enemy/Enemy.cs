using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ReboObject
{
    public float attackSpeed;
    public float timeCountToAttack;
    public float attackRate;

    public Level level;
    public HealthBar health;


    // Range move
    public Vector2 leftRangeMove;
    public Vector2 rightRangeMove;
    
    protected Transform player;
    protected Vector2 originalPos;

    protected EnemyAutoControl autoControl;


    private void Update()
    {
        autoControl.Execute();
        
        timeCountToAttack += Time.deltaTime;
    }


    public virtual void MoveToLef(bool left)
    {
        SetRun_Animation(true);
        MoveToLeft(left, runSpeed);
    }

    protected virtual Vector2 GetPushForceWhenAttacking()
    {
        float pushForce = 2 * attackSpeed;
        if (LookToTheLeft())
            pushForce = -pushForce;
        return new Vector2(pushForce, 0);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                       
                collision.gameObject.GetComponent<Character>().TakeDamage(GetAttackDamage());

               
                collision.gameObject.GetComponent<Character>().Move(GetPushForceWhenAttacking());
                
                break;
        }
    }

    public virtual void Chase()
    {
        SetRun_Animation(true);
        MoveToLef(vision.ShouldGoLeftToAttack(player.position, true));
    }

    public virtual IEnumerator DestroyEnemy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Destroy(gameObject);
    }
    //-----------------------------------------------
    public override void Idel()
    {
        SetRun_Animation(false);
    }

    public override void MoveToLeft(bool left, float force)
    {
        base.MoveToLeft(left, force);
    }

    protected override bool CanJump()
    {
        return true;
    }

    protected override void Jump(Vector2 force)
    {
        Move(force);
    }

    public override void Move(Vector2 force)
    {
        GetComponent<Rigidbody2D>().AddForce(force);
    }

    protected override void TakingDamage(float damage)
    {
        SetTakeDamage_Animation();
        health.TakeDamage(damage);
    }

    protected override void DidTakedDamage(float damage)
    {
        if (!isAlive())
        {
            Die();
        }
    }

    public override bool CanAttack()
    {
        return (timeCountToAttack >= attackRate);
    }

    protected override void WillAttack()
    {
        SetAttack_Animation();
        timeCountToAttack = 0;
    }

    protected override void Attacking()
    {
        MoveToLeft(LookToTheLeft(), attackSpeed);
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
        SetDie_Animation();
        health.gameObject.SetActive(false);
        //Destroy(health.gameObject);
    }

    protected override void DidDied()
    {
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
