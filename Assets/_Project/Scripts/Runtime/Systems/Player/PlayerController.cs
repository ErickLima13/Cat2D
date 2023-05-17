using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRigidBody2D;
    private PlayerAnimator playerAnimator;

    public float Horizontal { get; private set; }

    public bool IsOnTheGround { get; private set; }
    public bool IsAttacking { get; private set; }
    public bool IsFlying { get; private set; }

    [Range(0, 1000)] public float jumpForce;
    [Range(0, 50)] public float speed;
    public float delayAttackTime;

    public Transform groundCheckL;
    public Transform groundCheckR;
    public LayerMask groundLayer;

    private bool isLeft;

    [Header("Shoot Attack")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballPos;
    [SerializeField] private float speedBall;

    [Header("Fly Movement")]
    [SerializeField] private float gravityDefault;
    [SerializeField] private float flyGravity;

    [Header("Colliders")]
    [SerializeField] private CapsuleCollider2D capsuleFly;

    private void Initialization()
    {
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<PlayerAnimator>();

        gravityDefault = playerRigidBody2D.gravityScale;
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
        AttackWithHammer();
        ShootAttack();
        Jump();
        FloatMove();
        ControlGravity();
    }

    #region Utilities

    private void ChangeGravityScale(float gravity)
    {
        playerRigidBody2D.gravityScale = gravity;
    }

    private void ControlGravity()
    {
        if (IsOnTheGround)
        {
            capsuleFly.enabled = false;
            CancelFly();
        }
    }

    private void CancelFly()
    {
        IsFlying = false;
        ChangeGravityScale(gravityDefault);
    }


    #endregion

    #region Basic Movements

    private void MovementControl()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (isLeft && Horizontal > 0)
        {
            Flip();
        }

        if (!isLeft && Horizontal < 0)
        {
            Flip();
        }

        playerRigidBody2D.velocity = new(Horizontal * speed, playerRigidBody2D.velocity.y);
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
        IsOnTheGround = Physics2D.OverlapArea(groundCheckL.position, groundCheckR.position, groundLayer);

        Debug.DrawLine(groundCheckL.position, groundCheckR.position, Color.yellow);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsOnTheGround)
        {
            playerRigidBody2D.AddForce(new Vector2(0, jumpForce));
        }
    }

    #endregion

    #region Special Movements

    private void AttackWithHammer()
    {
        if (Input.GetButtonDown("Fire1") && !IsAttacking)
        {
            playerAnimator.StopWaiting();
            CancelFly();
            IsAttacking = true;
            playerAnimator.AnimatorSetTrigger("Hammer");
        }
    }

    private void ShootAttack()
    {
        if (Input.GetButtonDown("Fire2") && !IsAttacking)
        {
            playerAnimator.StopWaiting();
            CancelFly();
            IsAttacking = true;
            playerAnimator.AnimatorSetTrigger("Shoot");
        }
    }

    private void FloatMove()
    {
        if (Input.GetButtonDown("Jump") && !IsOnTheGround && !IsFlying && !IsAttacking)
        {
            capsuleFly.enabled = true;
            playerRigidBody2D.velocity = new(playerRigidBody2D.velocity.x, 0);
            IsFlying = true;
            ChangeGravityScale(flyGravity);
        }
    }

    public void OnAttackComplete()
    {
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delayAttackTime);
        IsAttacking = false;

    }

    public void SpawnShoot()
    {
        GameObject temp = Instantiate(ballPrefab, ballPos.position, Quaternion.identity);
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(speedBall, 0);
    }

    #endregion
}

