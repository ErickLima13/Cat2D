using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody2D;
    private Animator playerAnimator;

    private float horizontal;

    [SerializeField] private bool isOnTheGround;
    [SerializeField] private bool isWaiting;
    [SerializeField] private bool isAttacking;

    [Range(0, 1000)] public float jumpForce;
    [Range(0, 50)] public float speed;
    public float delayAttackTime;

    public Transform groundCheckL;
    public Transform groundCheckR;
    public LayerMask groundLayer;

    public bool isLeft;

    [Header("Shoot Attack")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballPos;
    [SerializeField] private float speedBall;

    private void Initialization()
    {
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }
    private void FixedUpdate()
    {
        GroundCheck();
    }

    // Update is called once per frame
    void Update()
    {
        MovementControl();
        Jump();
        AnimationsControl();
    }


    #region Basic Movements

    private void MovementControl()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isLeft && horizontal > 0)
        {
            Flip();
        }

        if (!isLeft && horizontal < 0)
        {
            Flip();
        }

        playerRigidBody2D.velocity = new(horizontal * speed, playerRigidBody2D.velocity.y);

        AttackWithHammer();
        ShootAttack();
    }

    private void Flip()
    {
        isLeft = !isLeft;
        float scaleX = transform.localScale.x;
        scaleX *= -1f;
        speedBall *= -1f;

        transform.localScale = new(scaleX, transform.localScale.y, transform.localScale.z);
    }

    
    private void GroundCheck()
    {
        isOnTheGround = Physics2D.OverlapArea(groundCheckL.position, groundCheckR.position, groundLayer);

        Debug.DrawLine(groundCheckL.position, groundCheckR.position, Color.yellow);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isOnTheGround)
        {
            playerRigidBody2D.AddForce(new Vector2(0, jumpForce));
        }
    }

    #endregion

    #region Special Movements

    private void AttackWithHammer()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            StopWaiting();
            isAttacking = true;
            playerAnimator.SetTrigger("Hammer"); 
        }
    }

    private void ShootAttack()
    {
        if (Input.GetButtonDown("Fire2") && !isAttacking)
        {
            StopWaiting();
            isAttacking = true;
            playerAnimator.SetTrigger("Shoot"); 
        }
    }

    public void OnAttackComplete()
    {
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delayAttackTime);
        isAttacking = false;

    }

    public void SpawnShoot()
    {
        GameObject temp = Instantiate(ballPrefab, ballPos.position, Quaternion.identity);
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(speedBall, 0);
    }

    #endregion

    #region Animation

    private void AnimationsControl()
    {
        playerAnimator.SetInteger("SpeedX", (int)horizontal);
        playerAnimator.SetBool("Grounded", isOnTheGround);
        playerAnimator.SetFloat("SpeedY", playerRigidBody2D.velocity.y);

        StartWaiting();
    }

    private void StartWaiting()
    {
        if (horizontal == 0 && isOnTheGround && !isWaiting)
        {
            isWaiting = true;
            StartCoroutine(TiredOfWaiting());
        }
        else if (horizontal != 0 || !isOnTheGround)
        {
            StopWaiting();
        }
    }

    private void StopWaiting()
    {
        isWaiting = false;
        StopCoroutine(TiredOfWaiting());
    }

    private IEnumerator TiredOfWaiting()
    {
        yield return new WaitForSeconds(8);
        playerAnimator.SetTrigger("Idle");
    }

    #endregion
}
