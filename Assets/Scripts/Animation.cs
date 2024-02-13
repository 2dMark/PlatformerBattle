using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Movement))]

public class Animation : MonoBehaviour
{
    private const string AnimatorState = "State";

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Movement _movement;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _movement = GetComponent<Movement>();
    }

    private void Update()
    {
        RefreshDirectionState();
        RefreshMoveState();
    }

    private void RefreshMoveState()
    {
        if (_movement.GetMoveState != (Movement.MoveState)_animator.GetInteger(AnimatorState))
            _animator.SetInteger(AnimatorState, (int)_movement.GetMoveState);
    }

    private void RefreshDirectionState()
    {
        if (_movement.GetDirectionState == Movement.DirectionState.Right && _spriteRenderer.flipX == true)
            _spriteRenderer.flipX = false;
        else if (_movement.GetDirectionState == Movement.DirectionState.Left && _spriteRenderer.flipX == false)
            _spriteRenderer.flipX = true;
    }
}