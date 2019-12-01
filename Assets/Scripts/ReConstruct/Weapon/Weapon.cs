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
    public float fireRate = 0.6f;
    public float damage = 0;
    public float bulletForce = 7;
    public float bulletRange = 7;
    public float timePrepareToShoot = 0.4f;
    public WeaponType type = WeaponType.Bow;

    public Bullet bullet;
    public RuntimeAnimatorController weaponSuit;

    private float timeCount;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
        FindObjectOfType<SoundManager>().PlayWeaponSound(audioSource, type);
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

        bulletClone.SetAtributes(bulletPos, character.GetAttackDamage(), force, bulletRange);

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