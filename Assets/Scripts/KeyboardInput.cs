using UnityEngine;

[RequireComponent(typeof(Movement))]

public class KeyboardInput : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    private Movement _movement;

    private Vector2 HorizontalDirection => new(Input.GetAxisRaw(Horizontal), 0);

    private void Start()
    {
        _movement = GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.GetAxisRaw(Horizontal) != 0)
            _movement.Move(HorizontalDirection);

        if (Input.GetKeyDown(KeyCode.Space))
            _movement.Jump();
    }
}