using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ReboObject
{
    public int jumpCount = 0;
    public int maxJumpCount = 1;
    public int numberOfCoins = 0;

    public LevelBar level;
    public HealthBar health;
    public WeaponManager weaponManager;

    public Dictionary<EnemyType, int> numberOfEnemiesAnnihilated = new Dictionary<EnemyType, int>();    

    protected override void Setup()
    {
        damage = new Damage(baseDamage, attackDamage);

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
            MoveToLeft(MoveLeftKey(), runSpeed);
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
        }
    }  

    public virtual void updateNumberOfEnemiesAnnihilated(EnemyType type)
    {
        if (numberOfEnemiesAnnihilated.ContainsKey(type))
            numberOfEnemiesAnnihilated[type] += 1;
        else
            numberOfEnemiesAnnihilated.Add(type, 1);
    }
    //---------------------------------------------------------------------

    public override void MoveToLeft(bool left, float force)
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

    public override void Recuperate(float blood)
    {
        if (blood > 0)
        {
            health.Recuperate(blood);
        }
    }

    protected override void WillTakeDamage(float damage)
    {
        
        SetTakeDamage_Animation();
    }

    protected override void TakingDamage(float damage)
    {
        health.TakeDamage(damage);
    }

    protected override void DidTakedDamage(float damage)
    {
        SetRun_Animation(false);
        SetJump_Animation(false);

        FindObjectOfType<SoundManager>().PlayPlayerSound(SoundType.BeAttacked);
        if (!isAlive())
            Die();
    }

    public override bool CanAttack()
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

    protected override void WillBeDied()
    {
        SetDie_Animation();
    }

    public override float GetAttackDamage()
    {
        return damage.GetAttackDamage() + level.GetAdditionDamage();
    }

    public override float GetComboDamage()
    {
        return GetAttackDamage() + damage.GetBaseDamage();
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

    private void SetTakeDamage_Animation()
    {
        GetComponent<Animator>().SetTrigger(AnimationConstants.CHARACTER_TAKING_DAMAGE);  
    }

    private void SetDie_Animation()
    {
        GetComponent<Animator>().SetTrigger(AnimationConstants.CHARACTER_TAKING_DAMAGE);
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
