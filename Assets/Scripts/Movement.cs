using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Movement : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _jumpForce;
    [SerializeField] private LayerMask _groundMask;

    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;
    private MoveState _moveState = MoveState.Idle;
    private DirectionState _directionState = DirectionState.Right;
    private bool _isJumped = false;
    private Vector3 _lastPosition;

    public MoveState GetMoveState => _moveState;

    public DirectionState GetDirectionState => _directionState;

    private bool IsGrounded => Physics2D.BoxCast(_boxCollider2D.bounds.center,
        _boxCollider2D.bounds.size, 0f, Vector2.down, .1f, _groundMask);

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.freezeRotation = true;
        _lastPosition = transform.position;
    }

    private void Update()
    {
        ResetIsJumpedState();
        RefreshMoveState();
    }

    public void Move(Vector2 direction)
    {
        direction = GetNormalizedHorizontalVector(direction);

        if (direction.x > 0 && _directionState != DirectionState.Right)
            _directionState = DirectionState.Right;
        else if (direction.x < 0 && _directionState != DirectionState.Left)
            _directionState = DirectionState.Left;

        transform.Translate(_speed * Time.deltaTime * direction);
    }

    public void Jump()
    {
        if (IsGrounded && _rigidbody2D.velocity.y == 0)
        {
            _rigidbody2D.velocity = Vector2.up * _jumpForce;
            _moveState = MoveState.Jump;
            _isJumped = true;
        }
    }

    private void ResetIsJumpedState()
    {
        if (_isJumped == true && _rigidbody2D.velocity.y < 0)
            _isJumped = false;
    }

    private void RefreshMoveState()
    {
        if (IsGrounded && _rigidbody2D.velocity == Vector2.zero)
        {
            if (_lastPosition.x == transform.position.x && _moveState != MoveState.Idle)
                _moveState = MoveState.Idle;
            else if (_lastPosition.x != transform.position.x && _moveState != MoveState.Run)
                _moveState = MoveState.Run;
        }
        else if (_isJumped == false && _moveState != MoveState.Fall)
        {
            _moveState = MoveState.Fall;
        }

        _lastPosition = transform.position;
    }

    private Vector2 GetNormalizedHorizontalVector(Vector2 direction)
    {
        if (direction.y != 0)
            direction.y = 0;

        if (direction.x < 1 && direction.x > 0)
            direction.x = 1;
        else if (direction.x > -1 && direction.x < 0)
            direction.x = -1;

        return direction;
    }

    public enum MoveState
    {
        Idle,
        Run,
        Jump,
        Fall,
    }

    public enum DirectionState
    {
        Left,
        Right,
    }
}