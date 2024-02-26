using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWalkPatrol : MonoBehaviour
{
    private GameObject player;
    public NavMeshAgent agent;
    private LayerMask groundMask;
    private LayerMask playerMask;
    // patrol
    private Vector3 destPoint;
    private bool walkPointSet;
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        groundMask = LayerMask.GetMask("Ground");
        playerMask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (!walkPointSet) SearchForDest();
        if (walkPointSet) agent.SetDestination(destPoint);
        if (Vector3.Distance(transform.position, destPoint) < 10) walkPointSet = false;
    }

    void SearchForDest()
    {
        float x = Random.Range(-range, range);
        float z = Random.Range(-range, range);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destPoint, Vector3.down, groundMask))
        {
            walkPointSet = true;
        }
    }
}
