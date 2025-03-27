using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    private float ySpeed;
    private CharacterController controller;
    Vector3 vel = Vector3.zero;
    Vector3 moveDirection = Vector3.zero;
    
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
        //    Vector3 moveDirection = new Vector3(horizontalMove, transform.position.y, verticalMove);
        //    moveDirection.Normalize();
        //    float magnitude = moveDirection.magnitude;
        //    magnitude = Mathf.Clamp01(magnitude);
        //    //transform.Translate(moveDirection * speed * Time.unscaledDeltaTime, Space.World);

        //    ySpeed += Physics.gravity.y * Time.unscaledDeltaTime;
        //    if (Input.GetButtonDown("Jump"))
        //    {
        //        ySpeed = -0.5f;
        //    }


        vel.y = -9.8f * Time.unscaledDeltaTime;
        controller.Move(vel * Time.unscaledDeltaTime);
        

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }

        
    }
}
