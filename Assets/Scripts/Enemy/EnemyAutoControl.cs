using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    MovingToLeft = 0,
    MovingToRight = 1,
    Idle = 2
}

public struct ActionTime
{
    public EnemyState state;
    public float maxTime;

    public ActionTime(EnemyState state, float maxTime)
    {
        this.state = state;
        this.maxTime = maxTime;
    }
}

public class EnemyAutoControl : Object
{
    protected Enemy enemy;
    protected float timeCount;

    protected EnemyState state;
    protected Transform playerTransform;
    protected ActionTime[] stateTimes;

    public EnemyAutoControl(Enemy enemy, Transform playerTransform, float maxTimeMoving, float maxTimeIdle)
    {
        this.enemy = enemy;
        this.playerTransform = playerTransform;

        this.state = Random.Range(1, 10) % 2 == 0 ? EnemyState.MovingToLeft : EnemyState.MovingToRight;
        this.timeCount = Random.Range(0, maxTimeMoving);

        stateTimes = new ActionTime[3] { new ActionTime(EnemyState.MovingToLeft, maxTimeMoving),
            new ActionTime(EnemyState.MovingToRight, maxTimeMoving),
            new ActionTime(EnemyState.Idle, maxTimeIdle)};
    }

    public virtual void Execute()
    {
        if (enemy.vision.TargetIsInRange(playerTransform.position) &&
            enemy.vision.SpottedOut(playerTransform.position) && !enemy.vision.OutOfRange())
        {
            if (enemy.CanAttack())
            {
                enemy.Attack();
            }
            else
            {
                enemy.Chase();
            }
        }
        else
        {
            AutoMove();
        }
        timeCount += Time.deltaTime;
    }

    protected virtual void AutoMove()
    {
        if (enemy.health.PercentageOfHealth() < 0.5)
        {
            ResetTimeCount();
            if (enemy.vision.ShouldGoLeftToAttack(playerTransform.position))
            {
                state = EnemyState.MovingToRight;
            }
            else
            {
                state = EnemyState.MovingToLeft;
            }
        }
        if (CanAction())
            KeepAction();
        else
        {
            if (timeCount >= stateTimes[(int)state].maxTime)
                ResetTimeCount();

            state = NextState();
        }
    }

    protected virtual void KeepAction()
    {
        if (state != EnemyState.Idle)
        {
            if (state == EnemyState.MovingToLeft)
                enemy.MoveToLef(true);
            else
                enemy.MoveToLef(false);
        }
        else
        {
            enemy.Idle();
            //Debug.Log("Keep Idel");
        }
    }

    protected virtual void ResetTimeCount()
    {
        timeCount = 0;
        //Debug.Log("Reset timecount");
    }

    protected virtual bool CanAction()
    {
        return timeCount < stateTimes[(int)state].maxTime
            && !(enemy.vision.OutOfLeftRange() && state == EnemyState.MovingToLeft)
            && !(enemy.vision.OutOfRightRange() && state == EnemyState.MovingToRight);
    }

    protected virtual EnemyState NextState()
    {
        EnemyState newState = EnemyState.Idle;
        switch (state)
        {
            case EnemyState.MovingToLeft:
                if (enemy.vision.OutOfLeftRange())
                    newState = EnemyState.Idle;
                break;

            case EnemyState.MovingToRight:
                if (enemy.vision.OutOfRightRange())
                    newState = EnemyState.Idle;
                break;

            case EnemyState.Idle:
                if (enemy.vision.OutOfRange())
                {
                    if (enemy.vision.OutOfLeftRange())
                        newState = EnemyState.MovingToRight;
                    else
                        newState = EnemyState.MovingToLeft;
                }
                else
                {
                    newState = (int)Random.Range(1, 10) % 2 == 0
                        ? EnemyState.MovingToLeft : EnemyState.MovingToRight;
                }

                break;
        }
        //Debug.Log("New state " + newState);
        return newState;
    }
}