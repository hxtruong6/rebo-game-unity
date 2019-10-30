using System;
using System.Collections;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;

    public float maxHealth = 100;
    private float currentHealth;
    private Animator animator;
    //private float delay = 0.5f;
    const string IS_DIE = "isDie";


    void Start()
    {
        bar = transform.Find("Bar");
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        ChangeHealth();
    }

    public void BeAttacked(float damage)
    {
        if (damage > 0)
        {
            bool characterDies = (currentHealth - damage) <= 0;
            currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
            //play sound
            //damageTextSpawner.Create(damage, transform.position);
            //var clip = damageSounds[Random.Range(0, damageSounds.Length)];
            //audioSource.PlayOneShot(clip);

            ChangeHealth();

            if (characterDies)
            {
                //Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
                StartCoroutine(KillCharacter());
            }
        }
    }


    IEnumerator KillCharacter()
    {
        //var aniLength = gameObject.GetComponent<Animation>()[DEATH_TRIGGER].length;
        animator.SetTrigger(IS_DIE);
        AnimatorStateInfo currInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(currInfo.length);
        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }


    public void Recuperate(float blood)
    {
        if (blood > 0)
        {
            currentHealth += blood;
            ChangeHealth();
        }
    }

    private void ChangeHealth()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        float bloodPecent = currentHealth / maxHealth;
        SetSize(bloodPecent);
    }

    private void SetSize(float size)
    {
        bar.localScale = new Vector3(size, 1f);
    }

    internal object Find(string v)
    {
        throw new NotImplementedException();
    }

    private void SetColor(Color color)
    {
        bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }


}
