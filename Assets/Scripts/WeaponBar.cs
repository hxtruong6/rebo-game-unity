using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WeaponBar : MonoBehaviour
{
    
    public Weapon[] weapons;
    private Weapon weapon;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeToWeapon(WeaponType.Machinegun);
    }

    public void ChangeToWeapon(WeaponType type)
    {
        if (weapon != null)
            weapon.gameObject.SetActive(false);
        for(int i=0; i<weapons.Length; i++)
            if (weapons[i].type == type)
            {                
                weapon = weapons[i];
                weapon.gameObject.SetActive(true);

                spriteRenderer.sprite = weapon.GetComponent<SpriteRenderer>().sprite;

                break;
            }
        
    }

    public void ChangeToOtherWeapon()
    {
        for(int i=0; i<weapons.Length; i++)
            if (weapon.type != weapons[i].type)
            {
                ChangeToWeapon(weapons[i].type);
                break;
            }
    }

    public Weapon GetWeapon()
    {
        return weapon;
    }
}
