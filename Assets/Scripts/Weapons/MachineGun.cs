using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    private void Awake()
    {
        fireRate = 0.3f;
        damage = 30;
        bulletForce = 15;
        bulletRange = 8;
        type = WeaponType.MACHINEGUN;
    }
}
