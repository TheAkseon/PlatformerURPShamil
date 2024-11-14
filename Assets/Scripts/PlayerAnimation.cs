using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator Animator;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    public void Walk(bool isWalking)
    {
        Animator.SetBool("IsWalking", isWalking);
    }

    public void Run(bool isRunning)
    {
        Animator.SetBool("IsRunning", isRunning);
    }

    public void Idle(bool isIdle)
    {
        Animator.SetBool("IsIdle", isIdle);
    }

    public void Jump(bool isJumping)
    {
        Animator.SetBool("IsJumping", isJumping);
    }
}
