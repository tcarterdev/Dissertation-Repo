using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Door : MonoBehaviour
{

    public Transform doorRayPoint;
    public Transform neighboringDoorPosition;
    public void CheckForNeighbor(LayerMask doorLayer, float maxNeighborDistance)
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(doorRayPoint.position, doorRayPoint.TransformDirection(Vector3.forward), out hit, 85, 7))
        {
            Debug.DrawRay(doorRayPoint.position, doorRayPoint.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
    }

    public void FixedUpdate()
    {
        
    }
}
