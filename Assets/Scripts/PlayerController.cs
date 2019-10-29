using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector3 movingPos;
    public float DAMGE = 20;
    [SerializeField] private float stepMoving;
    [SerializeField] private float jumpForce = 0.3f;
    private Transform _transform;
    //[SerializeField] float speed = 4.0f;
    [SerializeField] private int maxJumpCount = 2;
    public GameObject PlayerHitted;
    private bool isJumping = false;
    private bool isGrounded = false;
    private int jumpingCount = 0;
    public Transform groundCheck;
    public float jumpTimeDelay = 0.5f;
    private float jumpTimeCount = 0;
    private PlayerHealth playerHealth;

    public float HitTime = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        stepMoving = 0.05f;
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        playerHealth = GetComponent<PlayerHealth>();
        isJumping = true;
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        if (HorizontalMoving())
        {
            _animator.SetBool("isRunning", true);
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                _sprite.flipX = true;
                movingPos = _transform.position - new Vector3(stepMoving, 0, 0);
            }
            else
            {
                _sprite.flipX = false;
                movingPos = _transform.position + new Vector3(stepMoving, 0, 0);
            }
            _transform.position = movingPos;
            //_rb.AddForce(movement * speed * Time.deltaTime);

        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
    }

    private void CheckIfGrounded()
    {
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        //Debug.Log("xxx200 isGrouned: "+ isGrounded);
        if (isGrounded)
        {
            jumpingCount = 0;
            _animator.SetBool("isJumpEnd", true);
        }
        else
        {
            _animator.SetBool("isJumpEnd", false);
        }
    }

    private void FixedUpdate()
    {
        if (JumpingUpKeyPress())
        {
            Jump();
        }
        jumpTimeCount += Time.fixedDeltaTime;
        //Debug.Log("xxx003 jumpTimeCount: " + jumpTimeCount);
    }

    private void Jump()
    {
        if ((isGrounded && jumpingCount == 0) || (jumpingCount < maxJumpCount && jumpTimeCount >= jumpTimeDelay))
        {
            //Debug.Log("xxx001 jump count: " + jumpingCount);
            //Debug.Log("xxx002 isGround: " + isGrounded);
            jumpingCount++;
            jumpTimeCount = 0;

            _animator.SetTrigger("isJumping");
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }



    }
    private bool HorizontalMoving()
    {
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A);
    }

    private bool JumpingUpKeyPress()
    {
        return Input.GetKey(KeyCode.UpArrow)
            || Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.Space);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<HealthSystem>().TakeDamage(DAMGE);
            playerHealth.DecreaseTimesAlives();

            StartCoroutine(TurnOnPlayerHit());
        }
    }

    IEnumerator TurnOnPlayerHit()
    {
        PlayerHitted.SetActive(true);
        yield return new WaitForSeconds(HitTime);
        PlayerHitted.SetActive(false);
    }
}

