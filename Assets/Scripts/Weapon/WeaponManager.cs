using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : ReboRootObject
{
    public Weapon[] weapons;
    private Weapon weapon;
    private SpriteRenderer spriteRenderer;

    public override void SetupInAwake()
    {
        base.SetupInAwake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeToWeapon(weapons[0].type, null);
    }

    public void ChangeToWeapon(WeaponType type, Action<Weapon> callback)
    {
        if (weapon != null)
            weapon.gameObject.SetActive(false);

        for (int i = 0; i < weapons.Length; i++)
            if (weapons[i].type == type)
            {
                weapon = weapons[i];
                weapon.ResetTimeCount();
                weapon.gameObject.SetActive(true);

                spriteRenderer.sprite = weapon.GetComponent<SpriteRenderer>().sprite;

                if (callback != null)
                    callback(weapon);
                break;
            }
        
        weapon.gameObject.SetActive(true);
    }

    
    public void ChangeToOtherWeapon(Action<Weapon> callback)
    {
        for (int i = 0; i < weapons.Length; i++)
            if (weapon.type != weapons[i].type)
            {
                ChangeToWeapon(weapons[i].type, callback);
                break;
            }
    }

    public bool CanFire()
    {
        return weapon.CanFire();
    }

   public void Fire(Character character)
    {
        weapon.Fire(character);
    }

    public RuntimeAnimatorController GetWeaponSuit()
    {
        return weapon.weaponSuit;
    }
}
