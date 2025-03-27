using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationCalls : MonoBehaviour
{
    [SerializeField]
    private string AttackAnimname = string.Empty;
    [SerializeField]
    private UnityEvent _onAttack = new UnityEvent();
    [SerializeField]
    private UnityEvent _onAttackAnimationEnd = new UnityEvent();
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void DoAttackAnim()
    {
        _animator.Play(AttackAnimname);
    }

    public void DoAttack()
    {
        _onAttack?.Invoke();
    }

    public void AttackFinished()
    {
        _onAttackAnimationEnd?.Invoke();
    }
}
