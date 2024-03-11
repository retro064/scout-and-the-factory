using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public Transform GetWaypoint(int waypointIndex)
    {
        return transform.GetChild(waypointIndex);
    }

    public int GetNextWaypointIndex(int currentWaypointIndex)
    {
        int GetNextWaypointIndex = currentWaypointIndex + 1;
        
        if (GetNextWaypointIndex == transform.childCount)
        {
            GetNextWaypointIndex = 0;
        }

        return GetNextWaypointIndex;
    }
}
