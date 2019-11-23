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
        ChangeToWeapon(WeaponType.MACHINEGUN);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToWeapon(WeaponType type)
    {
        for(int i=0; i<weapons.Length; i++)
            if (weapons[i].type == type)
            {
                weapons[i].gameObject.SetActive(true);
                weapon = weapons[i];
                spriteRenderer.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
                //break;
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
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
