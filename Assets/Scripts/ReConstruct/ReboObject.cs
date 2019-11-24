using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboObject : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;

    protected BaseDamage damage;

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
        WillTakeDamage(damage);
        TakeDamage(damage);
        DidTakedDamage(damage);
    }

    protected virtual bool CanAttack()
    {
        return true;
    }

    protected virtual void Jump(Vector2 force)
    {

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
        WillAttack();
        Attacking();
        DidAttacked();
    }

    public virtual void Move(Vector2 force)
    {

    }

    public virtual void MoveTo(Vector2 newPos)
    {

    }

    public virtual void MoveToLeft(bool left, Vector2 force)
    {
        LookToTheLeft(left);
        if (left)
            force = -force;
        Move(force);
    }

    public virtual bool isAlive()
    {
        return true;
    }

    public virtual float GetCurrentDamage()
    {
        return 0;
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

    protected virtual void DiDDied()
    {

    }

    public virtual void Die()
    {
        WillBeDied();
        Dying();
        DiDDied();
    }
}
