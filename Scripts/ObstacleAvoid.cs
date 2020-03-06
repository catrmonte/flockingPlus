using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoid : SteeringBehavior
{
    float avoidDistance = 10f; // radius to avoid within, greater than character radius
    float maxAcceleration = 6f;
    float lookahead = 15f;  // distance to look ahead / raycast length

    public GameObject target;
    public Kinematic character;

    public override SteeringOutput getSteering()
    {
       RaycastHit hit;

        // If no collision deteted up to lookahead distance away in direction of linVelocity...
        if (!Physics.Raycast(character.transform.position, character.linearVelocity, out hit, lookahead))
        {
            // We don't need to do anything, return null
            //return null;
        }
        else
        {
            // change the target position to seek to
            if (hit.transform.gameObject.tag != "Bird")
            {
                Debug.Log(hit.transform.gameObject.tag);
                target.transform.position = hit.point + hit.normal * avoidDistance;
            }
            
        }

        SteeringOutput result = new SteeringOutput();
        result.linear = target.transform.position - character.transform.position;
        result.linear.Normalize();
        result.linear *= maxAcceleration;
        return result;
    }
}
