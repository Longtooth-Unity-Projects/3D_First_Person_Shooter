using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    private List<Transform> waypoints = new List<Transform>();

    private void Awake()
    {
        foreach (Transform point in transform)
        {
            waypoints.Add(point);
        }
    }


    public Transform GetRandomDestination()
    {
        return waypoints[Random.Range(0, waypoints.Count)];
    }



}
