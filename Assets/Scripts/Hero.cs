using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public float speed = 7;
    public float jumpSpeed = 200;
    public int jumpCount = 0;
    private const int maxJumpCount = 2;

    public LevelBar level;
    public HealthBar health;
    public WeaponBar weaponBar;


    public AnimationClip a;

    private SpriteRenderer characterSprite;
    private Damage damage;

    private const string RUN_ANIMATION = "isRunning";
    private const string SHOOT_ANIMATION = "isShooting";
    private const string JUMP_ANIMATION = "isJumping";
    private const string FALL_ANIMATION = "isFalling";

    void Start()
    {
        speed = 7f;
        jumpSpeed = 200f;
        jumpCount = 0;

        damage = new Damage();
        
        characterSprite = GetComponent<SpriteRenderer>();

        ChangeSuit();
    }


    void Update()
    {
        Interact();
    }

    private void Interact()
    {
        if (MoveLeftKey() || MoveRightKey())
        {
            SetRun_Animation(true);
                
            if (MoveRightKey())
            {
                characterSprite.flipX = false;

                Move(new Vector2(speed, 0));
            }
            else
            {
                characterSprite.flipX = true;

                Move(new Vector2(-speed, 0));
            }

        }
        else
        {
            SetRun_Animation(false);
        }

        if (JumpKey())
        {
            if (jumpCount < maxJumpCount)
            {
                SetJump_Animation(true);
                jumpCount++;
                Move(new Vector2(0, jumpSpeed));
            }   
        }
        

        if (ShootKey())
        {
            if (weaponBar.GetWeapon().CanShoot())
            {
                Shoot();                
            }     
        }
        

        if (ChangeWeaponKey())
        {
            ChangeWeapon();
        }
    }

    public void BeAttacked(float damage)
    {
        SetFall_Animation();
        health.BeAttacked(damage);
    }

    private void ChangeWeapon()
    {
        weaponBar.ChangeToOtherWeapon();
        ChangeSuit();
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
                BeAttacked(100);
                break;
        }
    }

    private void Move(Vector2 force)
    {
        GetComponent<Rigidbody2D>().AddForce(force);
    }

    private void SetRun_Animation(bool value)
    {
        GetComponent<Animator>().SetBool(RUN_ANIMATION, value);
    }

    private void SetFall_Animation()
    {
        GetComponent<Animator>().SetTrigger(FALL_ANIMATION);
        SetJump_Animation(false);
        SetRun_Animation(false);
    }

    private bool IsRunning()
    {
        return GetComponent<Animator>().GetBool(RUN_ANIMATION);
    }

    private void SetShoot_Animation(bool value)
    {
        GetComponent<Animator>().SetTrigger(SHOOT_ANIMATION);
    }

    private void SetJump_Animation(bool value)
    {
        GetComponent<Animator>().SetBool(JUMP_ANIMATION, value);
    }

    private void Shoot()
    {
        weaponBar.GetWeapon().PrepareToShoot();
        // Set animation
        SetShoot_Animation(true);
 
        StartCoroutine(ShootNow());
    }

    IEnumerator ShootNow()
    {
        
        yield return new WaitForSeconds(weaponBar.GetWeapon().GetPrepareTimeToShoot());

        //if (weaponBar.GetWeapon().CanShoot())
        {
            // Config Bullet
            Vector2 bulletPos = transform.position;
            Vector2 distanceBetweenBulletVsHero = new Vector2(1, 0);
            if (characterSprite.flipX)
            {
                bulletPos -= distanceBetweenBulletVsHero;
            }
            else
            {
                bulletPos += distanceBetweenBulletVsHero;
            }

            weaponBar.GetWeapon().Shoot(bulletPos, TotalDamage(), 0, characterSprite.flipX);
        }
        
    }

    private float TotalDamage()
    {
        return damage.GetCurrentDamage() + level.GetDamage();
    }

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

    private bool ShootKey()
    {
        return Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Space);
    }

    private bool ChangeWeaponKey()
    {
        return Input.GetKeyDown(KeyCode.C);
    }

    private void ChangeSuit()
    {
        GetComponent<Animator>().runtimeAnimatorController = weaponBar.GetWeapon().weaponSuit;
    }
    
    
}
