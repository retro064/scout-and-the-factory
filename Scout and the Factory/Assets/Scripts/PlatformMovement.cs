using Unity.VisualScripting;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField]
    private WaypointPath waypointPath;
    [SerializeField]
    private float speed;
    private int targetWaypointIndex;
    private Transform previousWaypoint;
    private Transform targetWaypoint;
    private float timeToWaypoint;
    private float elapsedTime;

    void Start()
    {
        TargetNextWayPoint();
    }

    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        float elapsedPercentage = elapsedTime / timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.SetPositionAndRotation(Vector3.Slerp(previousWaypoint.position, targetWaypoint.position, elapsedPercentage), Quaternion.Lerp(previousWaypoint.rotation, targetWaypoint.rotation, elapsedPercentage));
        if (elapsedPercentage >= 1)
        {
            TargetNextWayPoint();
        }
    }

    private void TargetNextWayPoint()
    {
        previousWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);
        targetWaypointIndex = waypointPath.GetNextWaypointIndex(targetWaypointIndex);
        targetWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);

        elapsedTime = 0;
        float distanceToWaypoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position);
        timeToWaypoint = distanceToWaypoint / speed; 
    }
}
