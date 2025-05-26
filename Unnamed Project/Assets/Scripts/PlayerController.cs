using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private Rigidbody rb;
    public Transform cam;
    private Vector3 movementDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        handleJump();
    }
    private void FixedUpdate()
    {
        handleRotation();
        handleMovement();
    }
    void handleMovement()
    {
        // Get Input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // Get Camera Vectors
        Vector3 cameraForward = cam.forward;
        Vector3 cameraRight = cam.right;

        // Flatten the vectors so the player doesn't move upward/downward
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Combine movement
        movementDirection = (cameraForward * moveZ + cameraRight * moveX).normalized;

        // Apply
        rb.linearVelocity = movementDirection * moveSpeed + new Vector3(0, rb.linearVelocity.y, 0);
    }
    void handleRotation()
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
    void handleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
