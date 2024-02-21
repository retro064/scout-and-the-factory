using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float runSpeed = 10.0f;
    private CharacterController controller;
    private Vector3 velocity;
    private Transform player;
    public float targetRadius = 15.0f;
    private const float GRAVITY = -9.81f; // * 2f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float targetDistance = Vector3.Distance(transform.position, player.position);

        if (targetDistance <= targetRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, runSpeed * Time.deltaTime);
        } else
        {
            switch (velocity)
            {

            }
        }
    }

    private void FixedUpdate()
    {
        velocity.y += GRAVITY * Time.fixedDeltaTime;
        // controller.Move(velocity);
    }
}
