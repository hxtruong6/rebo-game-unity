using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float currentHealthPoints;
    [SerializeField] float maxHealthPoints = 100f;
    public GameObject[] HealthBar = new GameObject[6];
    public float HitAniDelayTime = 0.5f;
    const string DEATH_TRIGGER = "Death";
    Animator animator;

    private int currHealthBar = 0;

    private int MAX_HEALTH_BAR = 6;

    // Start is called before the first frame update
    void Start()
    {
        currentHealthPoints = maxHealthPoints;
        animator = GetComponent<Animator>();
        //for (int i = 0; i < HealthBar.Length; i++)
        //{
        //    HealthBar[i].gameObject.SetActive(false);
        //}
        //HealthBar[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {

        bool characterDies = (currentHealthPoints - damage) <= 0;
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
        //play sound
        //damageTextSpawner.Create(damage, transform.position);
        //var clip = damageSounds[Random.Range(0, damageSounds.Length)];
        //audioSource.PlayOneShot(clip);
        ChangeHealthBar();

        if (characterDies)
        {
            //Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);

            StartCoroutine(KillCharacter());
        }
    }

    private void ChangeHealthBar()
    {
        if (currHealthBar >= MAX_HEALTH_BAR) return;
        HealthBar[currHealthBar].SetActive(false);
        currHealthBar++;
        HealthBar[currHealthBar].SetActive(true);
    }

    IEnumerator KillCharacter()
    {
        //var aniLength = gameObject.GetComponent<Animation>()[DEATH_TRIGGER].length;
        animator.SetTrigger(DEATH_TRIGGER);
        AnimatorStateInfo currInfo = animator.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(currInfo.length + HitAniDelayTime);
        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }

}
