using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : Item
{
    public int numberOfCoins = 1;

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

                collision.gameObject.GetComponent<Character>().numberOfCoins += numberOfCoins;

                Destroy(1f);

                break;
        }
    }
}
