using System;
using System.Collections;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;

    public float maxHealth = 100;
    private float currentHealth;
    private Animator animator;
    public float DestroyDelayTime = 0.5f;


    void Start()
    {
        bar = transform.Find("Bar");
        currentHealth = maxHealth;
        SetSize(1.0f);
        animator = GetComponentInParent<Animator>();
    }

    //private void Update()
    //{
    //    ChangeHealth();
    //}

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
                StartCoroutine(KillCharacter());
            }
        }
    }

    IEnumerator KillCharacter()
    {
        GetComponent<ChildObjects>().SetActive(false);
        animator.SetTrigger(AnimationName.IS_DYING);
        AnimatorStateInfo currInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(currInfo.length + DestroyDelayTime);
        gameObject.GetComponentInParent<Collider2D>().isTrigger = true;
        Destroy(transform.parent.gameObject);
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

    public bool IsAlive()
    {
        return currentHealth > 0;
    }


}
