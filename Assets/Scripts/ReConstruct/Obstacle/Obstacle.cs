using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : ReboRootObject
{
    public float damage;
    public Vector2 pushForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                if (collision.gameObject != null)
                {
                    collision.gameObject.GetComponent<Character>().Move(pushForce);

                    collision.gameObject.GetComponent<Character>().TakeDamage(damage);
                }

                break;

        }
    }
}
