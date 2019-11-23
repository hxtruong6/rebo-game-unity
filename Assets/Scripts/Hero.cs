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
            Debug.Log("key shoot");
            if (weaponBar.GetWeapon().CanShoot())
            {
                Debug.Log("can shoot");
                Shoot();                
            }     
        }
        

        if (ChangeWeaponKey())
        {
            weaponBar.ChangeToOtherWeapon();
            ChangeSuit();
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
        return damage.Attack();
    }

    private bool MoveLeftKey()
    {
        return Input.GetKey(KeyCode.LeftArrow);
    }

    private bool MoveRightKey()
    {
        return Input.GetKey(KeyCode.RightArrow);
    }

    private bool JumpKey()
    {
        return Input.GetKeyDown(KeyCode.UpArrow);
    }

    private bool ShootKey()
    {
        return Input.GetKey(KeyCode.H) || Input.GetKey(KeyCode.Space);
    }

    private bool ChangeWeaponKey()
    {
        return Input.GetKeyDown(KeyCode.N);
    }

    private void ChangeSuit()
    {
        GetComponent<Animator>().runtimeAnimatorController = weaponBar.GetWeapon().weaponSuit;
    }
    
    
}
