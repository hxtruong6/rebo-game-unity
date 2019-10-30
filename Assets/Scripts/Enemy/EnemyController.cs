using UnityEngine;

enum ENEMY_STATE
{
    FALL,
    IDLE,
    MOVING
}

enum MOVING_STATE
{
    LEFT,
    RIGHT
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] float maxDist;
    [SerializeField] float minDist;
    [SerializeField] float movingSpeed;
    [SerializeField] float idleTime;
    [SerializeField] ENEMY_STATE InitState;
    ENEMY_STATE EnemyState;

    float timeCount;
    SpriteRenderer _sprite;
    Animator animator;
    Vector3 initialPosition;
    MOVING_STATE direction;
    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
        direction = MOVING_STATE.LEFT;
        //maxDist += transform.position.x;
        //minDist -= transform.position.x;
        movingSpeed = 0.3f;
        timeCount = 0;
        EnemyState = InitState;
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (EnemyState)
        {
            case ENEMY_STATE.FALL:
                if (isGrounded)
                {
                    EnemyState = ENEMY_STATE.IDLE;
                    animator.SetTrigger("fallEnd");
                }
                break;
            case ENEMY_STATE.IDLE:
                timeCount += Time.deltaTime;
                if (timeCount >= idleTime)
                {
                    EnemyState = ENEMY_STATE.MOVING;
                    timeCount = 0;
                }
                break;
            case ENEMY_STATE.MOVING:
                Moving();
                break;
            default:
                break;
        }
    }

    private void Moving()
    {
        switch (direction)
        {
            case MOVING_STATE.LEFT:
                if (transform.position.x >= minDist)
                {
                    transform.position += Vector3.right * -movingSpeed * Time.deltaTime;
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
                if (transform.position.x <= maxDist)
                {
                    transform.position += Vector3.right * movingSpeed * Time.deltaTime;
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
}