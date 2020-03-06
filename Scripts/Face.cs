using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Seperate Dev

public class Face : MonoBehaviour
{
    public Kinematic character;
    public GameObject target;

    float maxRotation = 50f; // maxSpeed or max angular velocity

    // the radius for arriving at the target
    float targetRadius = 1f;

    // the radius for beginning to slow down
    float slowRadius = 10f;

    // the time over which to achieve target speed
    float timeToTarget = 0.1f;

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        // Vector3 from the character towards the target. Direction we want character to ultimately face
        Vector3 direction = target.transform.position - character.transform.position;

        // difference in angles between where the character is facing and towards the target
        float rotation = Vector3.SignedAngle(character.transform.forward, direction, Vector3.up); // degrees

        float rotationSize = Mathf.Abs(rotation);
        if (rotationSize < targetRadius)
        {
            return null;
        }

        // if we are outside the slow radius then use max rotation
        float targetRotation = 0.0f; // target angular velocity
        if (rotationSize > slowRadius)
        {
            targetRotation = maxRotation;
        }
        else
        {
            targetRotation = maxRotation * rotationSize / slowRadius;
        }

        // add direction back to our target angular velocity
        targetRotation *= rotation / rotationSize;

        // acceleration tries to get to the targetRotation
        result.angular = targetRotation - character.angularVelocity;
        result.angular /= timeToTarget;

        result.linear = Vector3.zero;

        return result;
    }
}
