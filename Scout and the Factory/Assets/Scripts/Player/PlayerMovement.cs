using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PlayerState {
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public static PlayerState currentState;
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
    private float horizontal;
    private float vertical;
    private Vector3 direction;

    void Start()
    {
        currentState = PlayerState.walk;
        controller = GetComponent<CharacterController>();
        groundCheck = GameObject.Find("GroundCheck").GetComponent<Transform>();
        groundMask = LayerMask.GetMask("Ground");
        
        jumpNum = jumpNumMax;
        /*
        camera orbits:
            top: 3, 3
            middle: 2, 6
            bottom: -0.5, 3
        */
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, 0f, vertical).normalized;        

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
    }

    void FixedUpdate()
    {
        if (direction.magnitude >= 0.1f && currentState == PlayerState.walk)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(speed * Time.deltaTime * moveDir.normalized);
        }

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
            jumpNum = jumpNumMax;
        }

        grounded = Physics.CheckSphere(groundCheck.position, distanceRadius, groundMask);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
