using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Movement : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed = 2f;
    [SerializeField, Min(0)] private float _jumpForce = 5.5f;
    [SerializeField, Min(0)] private float _attackSpeed = 1f;
    [SerializeField, Min(0)] private float _attackRange = 0.2f;
    [SerializeField, Min(0)] private int _attackDamage = 1;
    [SerializeField] private LayerMask _groundMask;

    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;
    private MoveStates _moveState = MoveStates.Idle;
    private DirectionStates _directionState = DirectionStates.Right;
    private Vector3 _lastPosition;
    private bool _isJumped = false;
    private float _attackTime = 0;

    public float AttackRange => _attackRange;

    public MoveStates MoveState => _moveState;

    public DirectionStates DirectionState => _directionState;

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
        RefreshMoveState();
        ResetIsJumpedState();
        RefreshAttackTime();
    }

    public void Move(Vector2 direction)
    {
        direction = GetNormalizedHorizontalVector(direction);

        if (direction.x > 0 && _directionState != DirectionStates.Right)
            _directionState = DirectionStates.Right;
        else if (direction.x < 0 && _directionState != DirectionStates.Left)
            _directionState = DirectionStates.Left;

        transform.Translate(_speed * Time.deltaTime * direction);
    }

    public void Jump()
    {
        if (IsGrounded && _rigidbody2D.velocity.y == 0)
        {
            _rigidbody2D.velocity = Vector2.up * _jumpForce;
            _moveState = MoveStates.Jump;
            _isJumped = true;
        }
    }

    public void Attack()
    {
        if (_attackTime > 0)
            return;

        RaycastHit2D[] _raycastHit2D;

        _moveState = MoveStates.Attack;
        _attackTime = _attackSpeed;

        if (_directionState == DirectionStates.Right)
            _raycastHit2D = Physics2D.BoxCastAll(_boxCollider2D.bounds.center,
        _boxCollider2D.bounds.size, 0f, Vector2.right, _attackRange);
        else
            _raycastHit2D = Physics2D.BoxCastAll(_boxCollider2D.bounds.center,
        _boxCollider2D.bounds.size, 0f, Vector2.left, _attackRange);

        foreach (RaycastHit2D cast in _raycastHit2D)
            if (cast.collider.TryGetComponent(out Health health))
                health.TakeDamage(_attackDamage);
    }

    private void RefreshMoveState()
    {
        if (IsGrounded && _rigidbody2D.velocity == Vector2.zero)
        {
            if (_lastPosition.x == transform.position.x && _moveState != MoveStates.Idle)
                _moveState = MoveStates.Idle;
            else if (_lastPosition.x != transform.position.x && _moveState != MoveStates.Run)
                _moveState = MoveStates.Run;
        }
        else if (_isJumped == false && _moveState != MoveStates.Fall)
        {
            _moveState = MoveStates.Fall;
        }

        _lastPosition = transform.position;
    }

    private void ResetIsJumpedState()
    {
        if (_isJumped == true && _rigidbody2D.velocity.y < 0)
            _isJumped = false;
    }

    private void RefreshAttackTime()
    {
        if (_attackTime > 0)
            _attackTime -= Time.deltaTime;
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

    public enum MoveStates
    {
        Idle,
        Run,
        Jump,
        Fall,
        Attack,
    }

    public enum DirectionStates
    {
        Left,
        Right,
    }
}