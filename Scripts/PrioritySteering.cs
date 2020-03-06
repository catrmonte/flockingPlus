using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrioritySteering : MonoBehaviour
{
    float epsilon = 1f;

    public BlendedSteeringDemo[] groups;

    public SteeringOutput getSteering()
    {
        SteeringOutput steering = new SteeringOutput();

        foreach (BlendedSteeringDemo group in groups)
        {
            steering = group.getSteering();

            // Check against the threshold
            if (steering.linear.magnitude > epsilon || Mathf.Abs(steering.angular) > epsilon)
            {
                return steering;
            }

            // If here, no group had a large enough acceleration, so return the small acceleration
            // from the final group
            return steering;
        }

        Debug.Log("No groups in groups");
        return null;
    }
}
