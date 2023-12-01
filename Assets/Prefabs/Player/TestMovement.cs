using UnityEngine;

public class TestMovement : MonoBehaviour
{
    [SerializeField] private float speedMultipler = 4f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Application.targetFrameRate = 144;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float xSpeed = Input.GetAxisRaw("Horizontal");
        float zSpeed = Input.GetAxisRaw("Vertical");
        Vector3 movement = (new Vector3(xSpeed, 0, zSpeed) * Time.deltaTime).normalized * speedMultipler;
        rb.velocity = movement;
    }
}
