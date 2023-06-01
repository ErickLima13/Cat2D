using System.Collections;
using UnityEngine;

public class SpecialMoves : MonoBehaviour
{
    private PlayerController player;
    private PlayerAnimator playerAnimator;

    public int damageHammer;

    [field: SerializeField] public bool IsAttacking { get; private set; }
    [field: SerializeField] public bool IsFlying { get; private set; }
    [field: SerializeField] public bool IsSwim { get; private set; }

    [Header("Power Ups")]
    [SerializeField] private bool ball;
    [SerializeField] private bool hammer;
    [SerializeField] private bool floatingCape;

    [Range(0, 1000)] public float swimForce;

    public float delayAttackTime;

    [Header("Shoot Attack")]
    [SerializeField] private Transform ballPos;
    private GameObject ballPrefab;

    [Header("Gravity Settings")]
    [SerializeField] private float newGravity;
    private float gravityDefault;
   

    [Header("Colliders")]
    private CapsuleCollider2D defaultCol;
    [SerializeField] private CapsuleCollider2D capsuleFly;
    [SerializeField] private CapsuleCollider2D capsuleSwim;


    private void Initialization()
    {
        ballPrefab = Resources.Load<GameObject>("Ball");

        player = GetComponent<PlayerController>();
        playerAnimator = GetComponent<PlayerAnimator>();
        defaultCol = GetComponent<CapsuleCollider2D>();

        gravityDefault = player.playerRigidBody2D.gravityScale;
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
            player.GroundCheck();
        }
        else
        {
            player.SetIsOnTheGround(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateColliders();

        if (!IsSwim)
        {
            if (ball)
            {
                ShootAttack();
            }

            if (hammer)
            {
                AttackWithHammer();
            }

            if (floatingCape)
            {
                FloatMove();
            }
        }
        else
        {
            SwimMove();
        }
    }

    private void UpdateColliders()
    {
        if (player.IsOnTheGround)
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

    private void ChangeGravityScale(float gravity)
    {
        player.playerRigidBody2D.gravityScale = gravity;
    }

    private void CancelFly()
    {
        IsFlying = false;
        ChangeGravityScale(gravityDefault);
    }

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
        if (Input.GetButtonDown("Jump") && ! player.IsOnTheGround && !IsFlying && !IsAttacking)
        {
            player.playerRigidBody2D.velocity = new( player.playerRigidBody2D.velocity.x, newGravity);
            IsFlying = true;
            ChangeGravityScale(newGravity);
        }
    }

    private void SwimMove()
    {
        if (Input.GetButtonDown("Jump"))
        {
            player.playerRigidBody2D.AddForce(new Vector2(0, swimForce));
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
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(player.speedBall, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Submerse":
                if (!IsSwim)
                {
                    IsFlying = false;
                    ChangeGravityScale(newGravity);
                    player.playerRigidBody2D.velocity = Vector2.zero;
                    IsSwim = true;
                }
                break;
        }


        if (collision.TryGetComponent(out Status status))
        {
            status.HealthChange(damageHammer);
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
