using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 5.0f;
    public float jumpHeight = 10.0f;
    private float gravity = -9.81f;
    private bool isGrounded;
    private Vector3 velocity;
    public Transform groundCheck;
    public LayerMask groundMask;
    private float distanceRadius = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // directional input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        controller.Move(direction * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") & isGrounded)
        {
            Debug.Log("Jump!");
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, distanceRadius, groundMask);
    }

    private void FixedUpdate()
    {
        velocity.y += gravity * Time.fixedDeltaTime;
        controller.Move(velocity * Time.fixedDeltaTime);
    }
}
