using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WeaponBar : MonoBehaviour
{
    
    public Weapon[] weapons;
    private Weapon weapon;
    private SpriteRenderer spriteRenderer;
    void Start()
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

    public Weapon GetWeapon()
    {
        return weapon;
    }
}
