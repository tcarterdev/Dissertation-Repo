using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Door : MonoBehaviour
{

    public Transform doorRayPoint;
    public Transform neighborDoor;
    private bool initialize = false;

    public void CheckForNeighbor(LayerMask doorLayer, float maxNeighborDistance)
    {
        initialize = true;
        RaycastHit hit;

        Vector3 thisPos = doorRayPoint.position;
        if (Physics.Raycast(thisPos, doorRayPoint.forward, out hit, maxNeighborDistance))
        {
            //Debug.DrawLine(thisPos, hit.point, Color.red, 100f);
            neighborDoor = hit.collider.transform.parent;
        }
        else
        {
            //Debug.DrawLine(doorRayPoint.position, doorRayPoint.forward * maxNeighborDistance, Color.yellow, 100f);
            Destroy(this.gameObject);
        }
    }
}
