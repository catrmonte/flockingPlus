using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathFollow : Arrive
{
    public GameObject[] path;
    public int pathIndex;
    public float radius = 1f;

    public override SteeringOutput getSteering()
    {

        // Set up intial waypoint target
        if (target == null)
        {
            pathIndex = 0;

            target = path[pathIndex];
        }

        Vector3 vecToTarget = target.transform.position - character.transform.position;
        float distanceToTarget = vecToTarget.magnitude;

        if (distanceToTarget < radius)
        {
            pathIndex++;

            if (pathIndex > path.Length -1)
            {
                pathIndex = 0;
            }

            target = path[pathIndex];
        }

        return base.getSteering();
    }
}
