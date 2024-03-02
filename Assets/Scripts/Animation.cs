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

    private void Awake()
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
        if (_movement.MoveState != (Movement.MoveStates)_animator.GetInteger(AnimatorState))
            _animator.SetInteger(AnimatorState, (int)_movement.MoveState);
    }

    private void RefreshDirectionState()
    {
        if (_movement.DirectionState == Movement.DirectionStates.Right)
            _spriteRenderer.flipX = false;
        else
            _spriteRenderer.flipX = true;
    }
}