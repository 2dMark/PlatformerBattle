using System;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(WayPointPatrol))]

public class PlayerFinder : MonoBehaviour
{
    private const string PlayerTag = "Player";

    [SerializeField, Min(0)] private float _viewingRange;

    public event Action PlayerFindgding;

    private Movement _movement;
    private WayPointPatrol _wayPointPatrol;
    private RaycastHit2D[] _raycastHit2D;
    private Transform _playerTransform;

    public Transform GetPlayerTransform => _playerTransform;

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _wayPointPatrol = GetComponent<WayPointPatrol>();
    }

    private void Update()
    {
        if (_playerTransform == null)
        {
            if (_movement.GetDirectionState == Movement.DirectionState.Right)
                _raycastHit2D = Physics2D.RaycastAll(transform.position, Vector2.right, _viewingRange);
            else
                _raycastHit2D = Physics2D.RaycastAll(transform.position, Vector3.left, _viewingRange);

            if (_raycastHit2D != null)
            {
                foreach (RaycastHit2D cast in _raycastHit2D)
                {
                    if (cast.collider.CompareTag(PlayerTag))
                    {
                        _playerTransform = cast.collider.transform;

                        PlayerFindgding?.Invoke();

                        _wayPointPatrol.enabled = false;
                    }
                }
            }
        }
    }
}