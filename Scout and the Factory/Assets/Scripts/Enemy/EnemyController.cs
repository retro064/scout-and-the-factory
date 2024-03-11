using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    private Transform target;
    private NavMeshAgent agent;
    private AIWalkPatrol walkPatrol;
    public float runSpeed = 15.0f;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        walkPatrol = GetComponent<AIWalkPatrol>();
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            GetComponent<NavMeshAgent>().speed = runSpeed;
        } else
        {
            walkPatrol.Patrol();
        }

        if (distance <= agent.stoppingDistance)
        {
            FaceTarget();
            // *** invoke enemy attack function here ***
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
