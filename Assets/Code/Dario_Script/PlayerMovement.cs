using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    public float gravity = -9.81f;
    private CharacterController controller;
    private Vector3 velocity;
    Vector3 moveDirection = Vector3.zero;
    private int jumpCount; 
    private const int maxJumpCount = 2; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontalMove, 0, verticalMove);
        controller.Move(moveDirection * speed * Time.unscaledDeltaTime);
   
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.unscaledDeltaTime);
        }

        if (controller.isGrounded)
        {
            print("CharacterController is grounded");
            jumpCount = 0;
            velocity.y = 0;
            if (Input.GetButtonDown("Jump")) 
            {
                Jump();
            }
        }

        else
        {
            if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
            {
               Jump();
            }
        }

        velocity.y += gravity * Time.unscaledDeltaTime;
        controller.Move(velocity * Time.unscaledDeltaTime);
    }

    private void Jump()

    {
        velocity.y = jumpSpeed;
        jumpCount++;
        Debug.Log($"Jump executed. Current jump count: {jumpCount}");
    }
}
