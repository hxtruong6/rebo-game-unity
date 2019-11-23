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
    float timeCount = 0;
    private float nextShootTime;

    private void Start()
    {
        timeCount = 0;
    }

    public Weapon(float rate, float damage, float bulletForce, WeaponType type)
    {
       
        this.fireRate = rate;
        this.damage = damage;
        this.bulletForce = bulletForce;
        this.type = type;
    }

    private void Update()
    {
        timeCount += Time.deltaTime;
        Debug.Log(timeCount);

    }

    public bool CanShoot()
    {
     
        
        if (timeCount > 2 )
        {
            timeCount = 0;
            return true;
        }
        return false;
    }

    public void PrepareToShoot()
    {
        //TODO: move to shoot
        GetComponent<SoundManager>().PlayWeaponSound(type);
       
        nextShootTime = Time.time + fireRate;
    }

    public Bullet Shoot(Vector2 startPos, float additionalDamage, float additionalForce, bool flipX)
    {
        //nextShootTime = Time.time + fireRate;
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

    public float GetPrepareTimeToShoot()
    {
        return 2;
    }
}