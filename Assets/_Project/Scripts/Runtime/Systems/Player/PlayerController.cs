using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRigidBody2D;

    public float Horizontal { get; private set; }

    [field: SerializeField] public bool IsOnTheGround { get; private set; }

    [Range(0, 1000)] public float jumpForce;
    [Range(0, 50)] public float speed;

    public Transform groundCheckL;
    public Transform groundCheckR;
    public LayerMask groundLayer;

    public float speedBall;

    private bool isLeft;

    public void SetIsOnTheGround(bool isOnTheGround)
    {
        IsOnTheGround = isOnTheGround;
    }

    private void Initialization()
    {
        playerRigidBody2D = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Awake()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        MovementControl();
        Jump();
    }

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


    public void GroundCheck()
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
}

