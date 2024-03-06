using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Transform groundCheck;
    public Transform cam;
    public float speed = 5.0f;
    public float jumpHeight = 10.0f;
    private const float GRAVITY = -9.81f * 2f;
    private bool grounded;
    private int jumpNumMax = 2;
    private int jumpNum;
    private Vector3 velocity;
    private LayerMask groundMask;
    private float distanceRadius = 0.2f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        groundCheck = GameObject.Find("GroundCheck").GetComponent<Transform>();
        groundMask = LayerMask.GetMask("Ground");
        Cursor.lockState = CursorLockMode.Locked;
        
        jumpNum = jumpNumMax;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // set player state to idle;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * GRAVITY);
            jumpNum--;
        } else if (Input.GetButtonDown("Jump") && jumpNum >= 1)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * GRAVITY);
            jumpNum--;
        }

        velocity.y += GRAVITY * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jumpNum = jumpNumMax;
        }

        grounded = Physics.CheckSphere(groundCheck.position, distanceRadius, groundMask);
    }
}
