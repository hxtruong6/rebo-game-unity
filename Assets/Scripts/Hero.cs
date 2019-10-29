using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public int jumpCount;
    private const int maxJumpCount = 2;
    public LevelBar level;
    public HealthBar health;

    private SpriteRenderer sprite;
    private Damage damage;

    void Start()
    {
        speed = 7f;
        jumpSpeed = 200f;
        jumpCount = 0;
      
        sprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        Interact();
    }

    private void Interact()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            SetRun(true);

            if (Input.GetKey(KeyCode.RightArrow))
            {
                sprite.flipX = false;

                GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0));
            }
            else
            {
                sprite.flipX = true;

                GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed, 0));
            }

        }
        else
        {
            SetRun(false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (jumpCount < maxJumpCount)
            {
                jumpCount++;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed));
            }
        }
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpCount = 0;
    }

    private void SetRun(bool value)
    {
        GetComponent<Animator>().SetBool("isRunning", value);
    }

    private void SetShoot(bool value)
    {
        GetComponent<Animator>().SetBool("isShooting", value);
    }
}
