using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Machinegun,
    Bow
}

public class Weapon : MonoBehaviour
{
    public float fireRate;
    public float damage;
    public float bulletForce;
    public float bulletRange;
    public WeaponType type;

    public Bullet bullet;
    public RuntimeAnimatorController weaponSuit;

    private float timeCount;

    private void Update()
    {
        timeCount += Time.deltaTime;
    }

    public bool CanShoot()
    {
        return timeCount >= fireRate;
    }

    public void PrepareToShoot()
    {
        timeCount = 0;
    }

    public Bullet Shoot(Vector2 startPos, float additionalDamage, float additionalForce, bool flipX)
    {
        Bullet bulletClone = Instantiate(bullet, startPos, Quaternion.identity);

        float force = this.bulletForce + additionalForce;
        if (flipX)
        {
            force = -force;
        }
        bulletClone.SetAtributes(startPos, additionalDamage + this.damage, force, bulletRange);

        bulletClone.GetComponent<SpriteRenderer>().flipX = flipX;

        return bullet;
    }

    public float GetPrepareTimeToShoot()
    {
        return fireRate;
    }
}