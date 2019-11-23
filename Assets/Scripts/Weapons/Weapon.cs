using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    MACHINEGUN,
    BOW
}

public class Weapon : MonoBehaviour
{
    public float fireRate;
    public float damage;
    public float bulletForce;

    public WeaponType type;
    public Bullet bullet;
    public RuntimeAnimatorController weaponSuit;

    private float nextShootTime;

    public Weapon(float rate, float damage, float bulletForce, WeaponType type)
    {
        
        fireRate = rate;
        this.damage = damage;
        this.bulletForce = bulletForce;
        this.type = type;
    }

    public bool CanShoot()
    {
        if (Time.time > nextShootTime)
        {
            return true;
        }
        return false;
    }


    public Bullet Shoot(Vector2 startPos, float additionalDamage, float additionalForce, bool flipX)
    {

        nextShootTime = Time.time + fireRate;

        Bullet bulletClone = Instantiate(bullet, startPos, Quaternion.identity);

        float force = this.bulletForce + additionalForce;
        if (flipX)
        {
            force = -force;
        }
        bulletClone.SetAtributes(startPos, this.damage + additionalDamage, force);

        bulletClone.GetComponent<SpriteRenderer>().flipX = flipX;

        return bullet;
    }

}