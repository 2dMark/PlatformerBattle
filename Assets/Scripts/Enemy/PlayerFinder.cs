using System;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(WayPointPatrol))]
[RequireComponent(typeof(BoxCollider2D))]

public class PlayerFinder : MonoBehaviour
{
    [SerializeField, Min(0)] private float _viewingRange;

    public event Action PlayerFindgding;

    private BoxCollider2D _boxCollider2D;
    private Movement _movement;
    private WayPointPatrol _wayPointPatrol;
    private Transform _playerTransform;
    private RaycastHit2D[] _raycastHit2D;

    public Transform PlayerTransform => _playerTransform;

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _movement = GetComponent<Movement>();
        _wayPointPatrol = GetComponent<WayPointPatrol>();
    }

    private void Update()
    {
        if (_playerTransform == null)
        {
            if (_movement.DirectionState == Movement.DirectionStates.Right)
                _raycastHit2D = Physics2D.BoxCastAll(_boxCollider2D.bounds.center,
        _boxCollider2D.bounds.size, 0f, Vector2.right, _viewingRange);
            else
                _raycastHit2D = Physics2D.BoxCastAll(_boxCollider2D.bounds.center,
        _boxCollider2D.bounds.size, 0f, Vector2.left, _viewingRange);

            if (_raycastHit2D == null)
                return;

            foreach (RaycastHit2D cast in _raycastHit2D)
                if (cast.collider.GetComponent<Player>())
                {
                    _playerTransform = cast.collider.transform;

                    PlayerFindgding?.Invoke();

                    _wayPointPatrol.enabled = false;
                }
        }
    }
}