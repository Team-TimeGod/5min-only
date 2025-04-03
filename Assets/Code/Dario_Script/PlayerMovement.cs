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

    [Header("Camera Ref.")]
    [SerializeField] private Transform _mainCamera;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //CANCRO CODE >=.=<
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        Vector3 verticalDirection = verticalMove * _mainCamera.forward;
        Vector3 horizontalDirection = horizontalMove * _mainCamera.right;
        horizontalDirection = new Vector3(horizontalDirection.x, 0, horizontalDirection.z);
        verticalDirection = new Vector3(verticalDirection.x, 0, verticalDirection.z);
        Vector3 verticalRotation = verticalMove * _mainCamera.right*-1;
        Vector3 horizontalRotation = horizontalMove * _mainCamera.forward;
        horizontalRotation = new Vector3(horizontalRotation.x, 0, horizontalRotation.z);
        verticalRotation = new Vector3(verticalRotation.x, 0, verticalRotation.z);
        Vector3 rotation = verticalRotation + horizontalRotation;

        //moveDirection = new Vector3(horizontalMove, 0, verticalMove);
        moveDirection = horizontalDirection + verticalDirection;
        controller.Move(moveDirection * speed * Time.unscaledDeltaTime);
   
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(rotation, Vector3.up);
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
