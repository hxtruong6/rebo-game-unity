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
    public float timePrepareToShoot;
    public WeaponType type;

    public Bullet bullet;
    public RuntimeAnimatorController weaponSuit;

    private float timeCount;

    void Update()
    {
        timeCount += Time.deltaTime;
    }

    public virtual bool CanFire()
    {
        return timeCount >= fireRate;
    }

    public virtual void ResetTimeCount()
    {
        timeCount = 0;
    }

    public virtual void Fire(Character character)
    {
        StartCoroutine(FiringProcess(character));
    }

    protected virtual Bullet CreateBullet()
    {
        return Instantiate(bullet);
    }

    protected virtual void WillFire(Character character)
    {
        timeCount = 0;
    }

    protected virtual Bullet FireNow(Character character)
    {
        Bullet bulletClone = CreateBullet();

        Vector2 bulletPos = character.transform.position;
        Vector2 distanceBetweenBulletVsHero = new Vector2(1, 0);

        float force = this.bulletForce;
        if (character.LookToTheLeft())
        {
            bulletPos -= distanceBetweenBulletVsHero;
            force = -force;
        }
        else
        {
            bulletPos += distanceBetweenBulletVsHero;
        }

        bulletClone.SetAtributes(bulletPos, character.GetCurrentDamage(), force, bulletRange);

        bulletClone.GetComponent<SpriteRenderer>().flipX = character.LookToTheLeft();

        return bulletClone;
    }

    protected virtual void DidFire(Bullet bullet)
    {

    }

    protected virtual IEnumerator FiringProcess(Character character)
    {
        WillFire(character);

        yield return new WaitForSeconds(GetPrepareTimeToShoot());

        Bullet bulletClone = FireNow(character);

        DidFire(bulletClone);
    }
    
    protected virtual float GetPrepareTimeToShoot()
    {
        return timePrepareToShoot;
    }
}