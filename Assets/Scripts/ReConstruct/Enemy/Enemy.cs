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

    void Start()
    {
        runSpeed = 5f;
        jumpSpeed = 250f;
        attackSpeed = 150f;

        timeCountToAttack = 0;
        attackRate = 1.8f;

        damage = new Damage(50, 100);
        level = new Level();
        vision = new Vision(transform, 3f, 2f, leftRangeMove, rightRangeMove);

        player = GameObject.FindGameObjectWithTag("Player").transform;
        characterSprite = GetComponent<SpriteRenderer>();

        originalPos = transform.position;

        autoControl = new EnemyAutoControl(this, player, 6, 3);
        
    }

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

    protected override void WillTakeDamage(float damage)
    {
        
    }

    protected override void TakingDamage(float damage)
    {
        health.TakeDamage(damage);
    }

    protected override void DidTakedDamage(float damage)
    {

    }

    public override bool CanAttack()
    {
        return (timeCountToAttack >= attackRate) &&
             (vision.CanAttack(player.position));
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

    //--------------------------------------------
    // Helper Method
    private void SetRun_Animation(bool value)
    {
        GetComponent<Animator>().SetBool(AnimationConstants.ENEMY_MOVING, value);
    }

    private void SetFall_Animation()
    {
        GetComponent<Animator>().SetTrigger(AnimationConstants.ENEMY_FALLING);
        SetJump_Animation(false);
        SetRun_Animation(false);
    }

    private bool IsRunning()
    {
        return GetComponent<Animator>().GetBool(AnimationConstants.ENEMY_MOVING);
    }

    private void SetAttack_Animation()
    {
        GetComponent<Animator>().SetTrigger(AnimationConstants.ENEMY_ATTACKING);
    }

    private void SetJump_Animation(bool value)
    {
        GetComponent<Animator>().SetBool(AnimationConstants.ENEMY_JUMPING, value);
    }    

}
