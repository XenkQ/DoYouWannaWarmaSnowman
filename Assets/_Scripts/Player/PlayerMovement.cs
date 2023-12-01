using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private Stat speed;
    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        speed = GetComponent<PlayerStats>().speed;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float inputHorizontal = _playerInputActions.Player.Move.ReadValue<Vector2>().x;
        float inputVertical = _playerInputActions.Player.Move.ReadValue<Vector2>().y;

        Vector3 moveVector = (((transform.right * inputHorizontal) + (transform.forward * inputVertical))).normalized * speed.GetValue();
        rb.velocity = moveVector;
    }
}

