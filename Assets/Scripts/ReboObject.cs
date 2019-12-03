using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboObject : ReboRootObject
{
    protected static float FPS = 50;

    public float runSpeed = 10;
    public float jumpSpeed = 250;

    public float maxHealth = 1000;
    public int maxLevel = 6;
    public float baseDamage = 0;
    public float attackDamage = 50;

    public Vision vision;

    protected Damage damage;
    protected SpriteRenderer characterSprite;

    private void Start()
    {
        Setup();
    }

    protected virtual void Setup()
    {

    }

    public virtual void Recuperate(float blood)
    {

    }

    protected virtual void WillTakeDamage(float damage)
    {

    }

    protected virtual void TakingDamage(float damage)
    {

    }

    protected virtual void DidTakedDamage(float damage)
    {

    }

    public void TakeDamage(float damage)
    {
        if (!isAlive())
            return;
        WillTakeDamage(damage);
        TakingDamage(damage);
        DidTakedDamage(damage);
    }

    
    protected virtual void Jump(Vector2 force)
    {

    }

    protected virtual void Jump(float force)
    {

    }

    public virtual bool CanAttack()
    {
        return true;
    }


    protected virtual void WillAttack()
    {
            
    }

    protected virtual void Attacking()
    {

    }

    protected virtual void DidAttacked()
    {

    }

    public void Attack()
    {
        if (!isAlive())
            return;
        WillAttack();
        Attacking();
        DidAttacked();
    }

    public virtual void Move(Vector2 force)
    {       
        GetComponent<Rigidbody2D>().AddForce(force * FPS * Time.deltaTime);
    }

    public virtual void MoveTo(Vector2 newPos)
    {

    }

    public virtual void MoveToLeft(bool left, float force)
    {
        LookToTheLeft(left);
        if (left)
            force = -force;
        Move(new Vector2(force, 0));
    }

    public virtual bool isAlive()
    {
        return true;
    }

    public virtual float GetAttackDamage()
    {
        return 0;
    }

    public virtual float GetBaseDamage()
    {
        return 0;
    }

    public virtual float GetComboDamage()
    {
        return GetAttackDamage() + GetBaseDamage();
    }

    public virtual bool LookToTheLeft()
    {
        return true;
    }

    protected virtual void LookToTheLeft(bool toLeft)
    {

    }

    protected virtual bool CanJump()
    {
        return true;
    }

    protected virtual void WillBeDied()
    {

    }

    protected virtual void Dying()
    {

    }

    protected virtual void DidDied()
    {

    }

    public virtual void Die()
    {       
        WillBeDied();
        Dying();
        DidDied();
    }

    public virtual void Idle()
    {

    }
}
