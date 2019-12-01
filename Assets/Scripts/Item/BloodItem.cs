using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodItem : Item
{
    public float blood = 200;

    protected override void Setup()
    {
        base.Setup();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                SetGone_Animation(true);

                collision.gameObject.GetComponent<Character>().Recuperate(blood);

                Destroy(1f);

                break;
        }
    }

    
}
