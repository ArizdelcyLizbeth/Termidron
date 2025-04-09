using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager game;
    public SwordCollector swordCollector; 
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float jumpForce = 419f;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (!game.IsGameInProgress()) return;

        float moveInput = Input.GetAxis("Vertical");
        float rotateInput = Input.GetAxis("Horizontal");

        Vector3 moveDirection = transform.forward * -moveInput * moveSpeed;
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotateInput * rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && swordCollector != null)
        {
            swordCollector.DropSword(); 
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
