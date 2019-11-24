using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ReboObject
{
    public int jumpCount;
    public int maxJumpCount;

    public LevelBar level;
    public HealthBar health;
    public WeaponManager weaponManager;

    private SpriteRenderer characterSprite;

    

    void Start()
    {

        speed = 7f;
        jumpSpeed = 200f;
        jumpCount = 0;
        maxJumpCount = 2;

        damage = new BaseDamage(50);

        characterSprite = GetComponent<SpriteRenderer>();

        UpdateSuit();
    }


    void Update()
    {
        Interact();
    }

    private void Interact()
    {
        if (MoveLeftKey() || MoveRightKey())
        {
            MoveToLeft(MoveLeftKey(), new Vector2(speed, 0));
        }
        else
        {
            SetRun_Animation(false);
        }

        if (JumpKey() && CanJump())
        {
            Jump(new Vector2(0, jumpSpeed));
        }

        if (AttackKey() && CanAttack())
        {
            Attack();
        }

        if (ChangeWeaponKey())
        {
            ChangeWeapon();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                jumpCount = 0;
                SetJump_Animation(false);
                break;

            case "Enemy":
                TakeDamage(100);
                break;
        }
    }

    //---------------------------------------------------------------------

    public override void MoveToLeft(bool left, Vector2 force)
    {
        SetRun_Animation(true);
        base.MoveToLeft(left, force);
    }

    protected override bool CanJump()
    {
        return jumpCount < maxJumpCount;
    }

    protected override void Jump(Vector2 force)
    {
        SetJump_Animation(true);

        jumpCount++;

        Move(force);
    }

    public override void Move(Vector2 force)
    {
        GetComponent<Rigidbody2D>().AddForce(force);
    }

    protected override void WillTakeDamage(float damage)
    {
        SetFall_Animation();
    }

    protected override void TakingDamage(float damage)
    {

        health.TakeDamage(damage);
    }

    protected override void DidTakedDamage(float damage)
    {

    }

    protected override bool CanAttack()
    {
        return weaponManager.CanFire();
    }

    protected override void WillAttack()
    {
        SetAttack_Animation();
    }

    protected override void Attacking()
    {
        weaponManager.Fire(GetComponent<Character>());
    }

    protected override void DidAttacked()
    {
        // TODO DidAttacked
    }


    public override float GetCurrentDamage()
    {
        return damage.GetCurrentDamage() + level.GetDamage();
    }

    protected override void LookToTheLeft(bool toLeft)
    {
        characterSprite.flipX = toLeft;
    }

    public override bool LookToTheLeft()
    {
        return characterSprite.flipX;
    }

    public override bool isAlive()
    {
        return health.IsAlive();
    }


    //---------------------------------------------------------------------
    // Helper Method
    private void UpdateSuit()
    {
        GetComponent<Animator>().runtimeAnimatorController = weaponManager.GetWeaponSuit();
    }

    private void ChangeWeapon()
    {
        weaponManager.ChangeToOtherWeapon((weapon) => { UpdateSuit(); });        
    }

    private void SetRun_Animation(bool value)
    {
        GetComponent<Animator>().SetBool(AnimationConstants.CHARACTER_MOVING, value);
    }

    private void SetFall_Animation()
    {
        GetComponent<Animator>().SetTrigger(AnimationConstants.CHARACTER_FALLING);
        SetJump_Animation(false);
        SetRun_Animation(false);
    }

    private bool IsRunning()
    {
        return GetComponent<Animator>().GetBool(AnimationConstants.CHARACTER_MOVING);
    }

    private void SetAttack_Animation()
    {
        GetComponent<Animator>().SetTrigger(AnimationConstants.CHARACTER_ATTACKING);
    }

    private void SetJump_Animation(bool value)
    {
        GetComponent<Animator>().SetBool(AnimationConstants.CHARACTER_JUMPING, value);
    }

    //---------------------------------------------------------------------
    // Controll
    private bool MoveLeftKey()
    {
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
    }

    private bool MoveRightKey()
    {
        return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
    }

    private bool JumpKey()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
    }

    private bool AttackKey()
    {
        return Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Space);
    }

    private bool ChangeWeaponKey()
    {
        return Input.GetKeyDown(KeyCode.C);
    }
}
