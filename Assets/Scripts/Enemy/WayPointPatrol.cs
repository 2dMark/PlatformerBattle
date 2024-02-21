using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Enemy))]

public class WayPointPatrol : MonoBehaviour
{
    [SerializeField] private Transform _path;

    private Movement _movement;
    private Transform[] _wayPoints;
    private int _wayPointIndex;

    private Vector2 WayPointDirection => (_wayPoints[_wayPointIndex].position - transform.position).normalized;

    private void Start()
    {
        _movement = GetComponent<Movement>();

        AddWaypoints();
    }

    private void Update()
    {   
        if (_wayPoints != null)
            _movement.Move(WayPointDirection);
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