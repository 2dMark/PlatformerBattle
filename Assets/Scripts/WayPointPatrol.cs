using UnityEngine;

[RequireComponent(typeof(Movement))]

public class WayPointPatrol : MonoBehaviour
{
    [SerializeField] private Transform _path;

    private Transform[] _wayPoints;
    private int _wayPointIndex;
    private Movement _movement;

    private Vector2 GetWayPointDirection => (_wayPoints[_wayPointIndex].position - transform.position).normalized;

    private void Start()
    {
        _movement = GetComponent<Movement>();

        AddWaypoints();
    }

    private void Update()
    {   
        if (_wayPoints != null)
            _movement.Move(GetWayPointDirection);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_wayPoints == null)
            return;

        _wayPoints[_wayPointIndex].TryGetComponent(out CircleCollider2D wayPointCollider);

        if (collision == wayPointCollider)
            SetNextWaypoint();
    }

    private void AddWaypoints()
    {
        if (_path == null)
            return;

        _wayPoints = new Transform[_path.childCount];

        for (int i = 0; i < _wayPoints.Length; i++)
            _wayPoints[i] = _path.GetChild(i);
    }

    private void SetNextWaypoint() => _wayPointIndex = ++_wayPointIndex % _wayPoints.Length;
}