using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ReboRootObject
{
    public float damage;
    public float force;
    public float range;
    public Vector2 originalPos;


    public override void SetupInAwake()
    {
        base.SetupInAwake();
        range = 1;
    }    

    public virtual void SetAtributes(Vector2 pos, float damage, float force, float range)
    {
        this.originalPos = pos;
        transform.position = this.originalPos;
        this.damage = damage;
        this.force = force;
        this.range = range;
    }

    void Start()
    {
        originalPos = transform.position;
        Move(new Vector2(force, 0));
    }

    protected virtual void Move(Vector2 forceVector)
    {
        GetComponent<Rigidbody2D>().AddForce(forceVector);
    }


    void Update()
    {
        if (Mathf.Abs(transform.position.x - originalPos.x) >= range)
        {
            Destroy(gameObject);
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        WillOnTriggerEnter();

        switch (collision.gameObject.tag)
        {
            case "Enemy":
                if (collision != null && collision.gameObject != null && collision.gameObject.GetComponent<Enemy>().isAlive())
                {
                    collision.gameObject.GetComponent<Enemy>().TakeDamage(GetCurrentDamageFor(collision.gameObject));

                    Destroy(gameObject);
                }
                
                break;

        }

        DidOnTriggerEnter();
    }

    protected virtual void WillOnTriggerEnter()
    {

    }

    protected virtual void DidOnTriggerEnter()
    {
        
    }

    protected virtual float GetCurrentDamageFor(GameObject enemy)
    {
        float distance = 1 - Mathf.Abs(transform.position.x - originalPos.x) / range;
        if (distance < 0.3)
            distance = 0.3f;

        return damage * distance;
    }

}
