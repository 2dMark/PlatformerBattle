using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(PlayerFinder))]

public class PlayerPursuer : MonoBehaviour
{
    private Movement _movement;
    private PlayerFinder _playerFinder;
    private Transform _playerTransform;

    private Vector2 GetPlayerDirection => (_playerTransform.position - transform.position).normalized;

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _playerFinder = GetComponent<PlayerFinder>();
        _playerFinder.PlayerFindgding += SetPlayerTransformFromPlayerFinder;
    }

    private void Update()
    {
        if (_playerTransform != null)
            _movement.Move(GetPlayerDirection);
    }

    private void OnDisable()
    {
        _playerFinder.PlayerFindgding -= SetPlayerTransformFromPlayerFinder;
    }

    private void SetPlayerTransformFromPlayerFinder() => _playerTransform = _playerFinder.GetPlayerTransform;
}