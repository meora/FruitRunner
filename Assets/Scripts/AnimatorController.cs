using System.Collections;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Jump()
    {
        _animator.ResetTrigger("RunTrigger");
        _animator.SetTrigger("JumpTrigger");
    }

    public void Run()
    {
        _animator.ResetTrigger("JumpTrigger");
        _animator.SetTrigger("RunTrigger");
    }
}
