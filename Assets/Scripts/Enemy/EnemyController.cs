using UnityEngine;

enum ENEMY_STATE
{
    IDLE,
    MOVING,
    ATTACK,
    CHASING
}

enum MOVING_STATE
{
    LEFT,
    RIGHT
}

public enum EnemyType
{
    Solider,
    Slug
}

public class EnemyController : MonoBehaviour
{
    public EnemyType EnemyType;
    [SerializeField] float MaxDist;
    [SerializeField] float MinDist;
    [SerializeField] float MovingSpeed = 0.5f;
    [SerializeField] float IdleTime = 4f;
    [SerializeField] float ChaseRange = 5f;
    public float ChasingSpeed = 0.2f;
    public float BoudingMinX;
    public float BoudingMaxX;


    ENEMY_STATE EnemyState;

    float timeCount;
    SpriteRenderer _sprite;
    Animator animator;
    MOVING_STATE direction;
    bool isGrounded;
    float distanceToPlayer = float.MaxValue;
    Transform player;
    public float ThresholdAttack = 0.5f;
    HealthManager healthBar;
    private AudioSource _audioSource;
    private SoundManager _soundManager;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        direction = MOVING_STATE.LEFT;
        timeCount = 0;
        EnemyState = ENEMY_STATE.IDLE;
        isGrounded = false;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        healthBar = GetComponentInChildren<HealthManager>();
        _audioSource = GetComponent<AudioSource>();
        _soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!healthBar.IsAlive()) return;
        distanceToPlayer = Vector2.Distance(player.position, transform.position);
        switch (EnemyState)
        {
            case ENEMY_STATE.IDLE:
                if (distanceToPlayer <= ChaseRange)
                {
                    EnemyState = ENEMY_STATE.CHASING;
                }
                else
                {
                    timeCount += Time.deltaTime;
                    if (timeCount >= IdleTime)
                    {
                        EnemyState = ENEMY_STATE.MOVING;
                        timeCount = 0;
                    }
                }

                break;
            case ENEMY_STATE.MOVING:
                if (distanceToPlayer <= ChaseRange)
                {
                    EnemyState = ENEMY_STATE.CHASING;
                }
                else
                {
                    Moving();
                }

                break;
            case ENEMY_STATE.ATTACK:
                if (distanceToPlayer > ThresholdAttack)
                {
                    EnemyState = ENEMY_STATE.CHASING;
                }
                else
                {
                    animator.SetTrigger(AnimationName.IS_ATTACKING);
                }

                break;
            case ENEMY_STATE.CHASING:
                if (distanceToPlayer <= ThresholdAttack)
                {
                    EnemyState = ENEMY_STATE.ATTACK;
                }
                else if (distanceToPlayer > ChaseRange)
                {
                    EnemyState = ENEMY_STATE.IDLE;
                    animator.SetBool(AnimationName.IS_MOVING, false);
                }
                else
                {
                    animator.SetBool(AnimationName.IS_MOVING, true);
                    ChasingState();
                }

                break;

            default:
                break;
        }
    }

    private void ChasingState()
    {
        // TODO: if reach limit set idle
        Vector3 vectorDirection = Vector3.left;
        _sprite.flipX = false;
        if (player.position.x > transform.position.x)
        {
            vectorDirection = Vector3.right;
            _sprite.flipX = true;
        }

        Vector3 newPos = transform.position + vectorDirection * (MovingSpeed + ChasingSpeed) * Time.deltaTime;
        newPos.x = Mathf.Clamp(newPos.x, BoudingMinX, BoudingMaxX);
        transform.position = newPos;
    }

    private void Moving()
    {
        switch (direction)
        {
            case MOVING_STATE.LEFT:
                if (transform.position.x >= MinDist)
                {
                    transform.position += Vector3.left * MovingSpeed * Time.deltaTime;
                    _sprite.flipX = false;
                    animator.SetBool("isMoving", true);
                }
                else
                {
                    direction = MOVING_STATE.RIGHT;
                    EnemyState = ENEMY_STATE.IDLE;
                    animator.SetBool("isMoving", false);
                }

                break;
            case MOVING_STATE.RIGHT:
                if (transform.position.x <= MaxDist)
                {
                    transform.position += Vector3.right * MovingSpeed * Time.deltaTime;
                    _sprite.flipX = true;
                    animator.SetBool("isMoving", true);
                }
                else
                {
                    direction = MOVING_STATE.LEFT;
                    EnemyState = ENEMY_STATE.IDLE;
                    animator.SetBool("isMoving", false);
                }

                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    public void BePushed(float force)
    {
    }

    public void BeingAttacked(float damage)
    {
        _soundManager.PlayEnemySound(_audioSource,SoundType.BeAttacked, EnemyType);
        animator.SetTrigger(AnimationName.IS_ATTACKED);
        GetComponentInChildren<HealthManager>().BeAttacked(damage);
    }

    public void DieBehavior()
    {
        _soundManager.PlayEnemySound(_audioSource, SoundType.Die, EnemyType);
    }
}