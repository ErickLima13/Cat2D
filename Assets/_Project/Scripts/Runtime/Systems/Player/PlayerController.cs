using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRigidBody2D;
    private PlayerAnimator playerAnimator;

    public float Horizontal { get; private set; }

    [field: SerializeField] public bool IsOnTheGround { get; private set; }
    [field: SerializeField] public bool IsAttacking { get; private set; }
    [field: SerializeField] public bool IsFlying { get; private set; }
    [field: SerializeField] public bool IsSwim { get; private set; }

    [Range(0, 1000)] public float swimForce;
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

    [Header("Gravity Settings")]
    [SerializeField] private float gravityDefault;
    [SerializeField] private float newGravity;

    [Header("Colliders")]
    private CapsuleCollider2D defaultCol;
    [SerializeField] private CapsuleCollider2D capsuleFly;
    [SerializeField] private CapsuleCollider2D capsuleSwim;


    private void Initialization()
    {
        defaultCol = GetComponent<CapsuleCollider2D>();
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
        if (!IsSwim)
        {
            GroundCheck();
        }
        else
        {
            IsOnTheGround = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovementControl();
        UpdateColliders();

        if (!IsSwim)
        {
            AttackWithHammer();
            ShootAttack();
            Jump();
            FloatMove();
        }
        else
        {
            SwimMove();
        }
    }

    #region Utilities

    private void ChangeGravityScale(float gravity)
    {
        playerRigidBody2D.gravityScale = gravity;
    }

    private void UpdateColliders()
    {
        if (IsOnTheGround)
        {
            defaultCol.enabled = true;
            capsuleFly.enabled = false;
            capsuleSwim.enabled = false;
            CancelFly();
        }
        else if (IsFlying)
        {
            capsuleFly.enabled = true;
            defaultCol.enabled = false;
            capsuleSwim.enabled = false;
        }
        else if (IsSwim)
        {
            capsuleSwim.enabled = true;
            defaultCol.enabled = false;
            capsuleFly.enabled = false;
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
            playerRigidBody2D.velocity = new(playerRigidBody2D.velocity.x, newGravity);
            IsFlying = true;
            ChangeGravityScale(newGravity);
        }
    }

    private void SwimMove()
    {
        if (Input.GetButtonDown("Jump"))
        {
            playerRigidBody2D.AddForce(new Vector2(0, swimForce));
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Submerse":
                if (!IsSwim)
                {
                    IsFlying = false;
                    ChangeGravityScale(newGravity);
                    playerRigidBody2D.velocity = Vector2.zero;
                    IsSwim = true;
                }
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Water":
                CancelFly();
                IsSwim = false;
                break;
        }
    }
}

