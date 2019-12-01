using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : ReboRootObject
{
    public float damage = 100;
    public float maxTimeBetween2Attack = 0.7f;

    protected float timeCount = 0;

    private void Update()
    {
        timeCount += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                if (collision.gameObject != null)
                {
                    timeCount = 0;
                    collision.gameObject.GetComponent<Character>().TakeDamage(damage);
                }

                break;

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                if (timeCount > maxTimeBetween2Attack && collision.gameObject != null)
                {
                    timeCount = 0;
                    collision.gameObject.GetComponent<Character>().TakeDamage(damage);
                }

                break;

        }
    }
}
