using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    private void Awake()
    {
        fireRate = 0.4f;
        damage = 50;
        bulletForce = 7;
        bulletRange = 7;
        type = WeaponType.BOW;
    }

}
