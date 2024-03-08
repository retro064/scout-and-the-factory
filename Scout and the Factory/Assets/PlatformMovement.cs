using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private bool isHorizontal = true;
    [SerializeField] private float travelTime = 5.0f; // in seconds

    private Vector3 velocity;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (isHorizontal)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
    }
}
