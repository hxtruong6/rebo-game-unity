using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public float force;
    public Vector2 originalPos;
    
    public void SetAtributes(Vector2 pos, float damage, float force)
    {
        this.originalPos = pos;
        transform.position = this.originalPos;
        this.damage = damage;
        this.force = force;
    }
  
    void Start()
    {
        originalPos = transform.position;
        Move(new Vector2(force, 0));
    }

    private void Move(Vector2 forceVector)
    {
        GetComponent<Rigidbody2D>().AddForce(forceVector);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - originalPos.x) >= 20)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Damage :"+ this.damage);
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                collision.gameObject.GetComponent<EnemyController>().BeingAttacked(this.damage);
                break;
            
        }
        Destroy(gameObject);
    }

}
