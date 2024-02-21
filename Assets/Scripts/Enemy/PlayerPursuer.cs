using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(PlayerFinder))]

public class PlayerPursuer : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;
    private Movement _movement;
    private PlayerFinder _playerFinder;
    private Transform _playerTransform;
    private RaycastHit2D[] _raycastHit2D;

    private Vector2 PlayerDirection => (_playerTransform.position - transform.position).normalized;

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _movement = GetComponent<Movement>();
        _playerFinder = GetComponent<PlayerFinder>();

        _playerFinder.PlayerFindgding += SetPlayerTransformFromPlayerFinder;
    }

    private void Update()
    {
        if (_playerTransform != null)
        {
            _movement.Move(PlayerDirection);

            _raycastHit2D = Physics2D.BoxCastAll(_boxCollider2D.bounds.center,
        _boxCollider2D.bounds.size, 0f, Vector2.right, _movement.AttackRange);

            foreach (RaycastHit2D cast in _raycastHit2D)
                if (cast.collider.GetComponent<Player>())
                    _movement.Attack();
        }
    }

    private void OnDisable()
    {
        _playerFinder.PlayerFindgding -= SetPlayerTransformFromPlayerFinder;
    }

    private void SetPlayerTransformFromPlayerFinder() => _playerTransform = _playerFinder.PlayerTransform;
}