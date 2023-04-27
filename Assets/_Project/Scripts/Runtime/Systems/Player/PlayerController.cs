using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody2D;
    private Animator playerAnimator;

    private float horizontal;

    [SerializeField] private bool isOnTheGround;

    [Range(0, 1000)] public float jumpForce;
    [Range(0, 50)] public float speed;

    public Transform groundCheckL;
    public Transform groundCheckR;
    public LayerMask groundLayer;

    public bool isLeft;

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
    }

    private void Flip()
    {
        isLeft = !isLeft;
        float scaleX = transform.localScale.x;
        scaleX *= -1f;
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

    private void AnimationsControl()
    {
        playerAnimator.SetInteger("SpeedX", (int)horizontal);
        playerAnimator.SetBool("Grounded", isOnTheGround);
        playerAnimator.SetFloat("SpeedY", playerRigidBody2D.velocity.y);
    }

}
