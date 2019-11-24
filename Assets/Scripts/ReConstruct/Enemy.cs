using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ReboObject
{

    public LevelBar level;
    public HealthBar health;
    public WeaponManager weaponManager;

    private SpriteRenderer characterSprite;

    public override void MoveToLeft(bool left, Vector2 force)
    {
        SetRun_Animation(true);
        base.MoveToLeft(left, force);
    }

    protected override bool CanJump()
    {
        return true;
    }

    protected override void Jump(Vector2 force)
    {
        SetJump_Animation(true);

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
