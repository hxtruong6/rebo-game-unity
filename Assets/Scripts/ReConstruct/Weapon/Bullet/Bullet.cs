using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float force;
    public float range;
    public Vector2 originalPos;

    private void Awake()
    {
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

    // Update is called once per frame
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

                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);

                break;

        }

        DidOnTriggerEnter();
    }

    protected virtual void WillOnTriggerEnter()
    {

    }

    protected virtual void DidOnTriggerEnter()
    {
        Destroy(gameObject);
    }

    protected virtual float GetCurrentDamage()
    {
        return damage * (1 - Mathf.Abs(transform.position.x - originalPos.x) / range);
    }

}
