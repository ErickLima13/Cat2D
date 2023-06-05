using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerController player;
    private SpecialMoves playerSpecial;

    public bool IsWaiting { get; private set; }

    private void Initialization()
    {
        player = GetComponent<PlayerController>();
        playerSpecial = GetComponent<SpecialMoves>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        Initialization();
    }

    void Update()
    {
        AnimationsUpdate();
    }

    public void AnimatorSetTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    private void AnimationsUpdate()
    {
        animator.SetInteger("SpeedX", (int)player.Horizontal);

        animator.SetBool("Grounded", player.IsOnTheGround);
        animator.SetBool("isFly", playerSpecial.IsFlying);
        animator.SetBool("isAttacking", playerSpecial.IsAttacking);
        animator.SetBool("isSwim", playerSpecial.IsSwim);

        animator.SetFloat("SpeedY", player.playerRigidBody2D.velocity.y);

        StartWaiting();
    }


    private void StartWaiting()
    {
        if (player.Horizontal == 0 && player.IsOnTheGround && !IsWaiting)
        {
            IsWaiting = true;
            StartCoroutine(TiredOfWaiting());
        }
        else if (player.Horizontal != 0 || !player.IsOnTheGround)
        {
            StopWaiting();
        }
    }

    public void StopWaiting()
    {
        IsWaiting = false;
        StopCoroutine(TiredOfWaiting());
    }

    private IEnumerator TiredOfWaiting()
    {
        yield return new WaitForSeconds(8);
        animator.SetTrigger("Idle");
    }
}
