using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : Object
{
    private float spottOutRange;
    private float attackRange;

    private Transform owner;
    private Vector2 leftRange;
    private Vector2 rightRange;

    public Vision(Transform owner, float spottOutRange, float attackRange, Vector2 leftRange, Vector2 rightRange)
    {
        this.owner = owner;
        this.spottOutRange = spottOutRange;
        this.attackRange = attackRange;
        this.leftRange = leftRange;
        this.rightRange = rightRange;
    }

    public bool SpottedOut(Vector2 pos)
    {
        return Vector2.Distance(owner.position, pos) <= spottOutRange;
    }

    public bool ShouldGoLeftToAttack(Vector2 pos)
    {
        return pos.x < owner.position.x;
    }

    public bool TargetIsInRange(Vector2 pos)
    {
        return  leftRange.x <= pos.x &&  pos.x <= rightRange.x;
    }

    public bool CanAttack(Vector2 pos)
    {
        return !OutOfRange() && Vector2.Distance(owner.position, pos) <= attackRange;
    }

    public bool CanRunToLeft(bool toLeft)
    {
        if (toLeft)
        {
            if (OutOfLeftRange())
                return false;
            return true;
        }
        else
        {
            if (OutOfRightRange())
                return false;
            return true;
        }
    }

    public bool OutOfLeftRange()
    {
        return owner.position.x <= leftRange.x;
    }

    public bool OutOfRightRange()
    {
        return owner.position.x >= rightRange.x;
    }

    public bool OutOfRange()
    {
        return OutOfLeftRange() || OutOfRightRange();
    }
}
