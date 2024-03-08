using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Transform target;
    public static bool inBound;
    private bool inState = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // float targetDistance = Vector3.Distance(transform.position, target.position);
        if ((Input.GetButtonDown("Interact") && inBound) && !inState)
        {
            NPCDialog.talking = true;
            inState = true;
            // ***lock the player's movement***
        } else if ((Input.GetButtonDown("Interact") && NPCDialog.talking) && inState)
        {
            NPCDialog.talking = false;
            inState = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("Player hit enemy");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inBound = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inBound = false;
    }
}
