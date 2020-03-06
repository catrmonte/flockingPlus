using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// New

public class LWYG : SteeringBehavior
{
    public Kinematic character;

    float maxAngularAcceleration = 20f; // 10
    float maxRotation = 50f; // maxSpeed or max angular velocity

    // the radius for arriving at the target
    float targetRadius = 1f; // maybe better word is tolerance

    // the radius for beginning to slow down
    float slowRadius = 10f; // 2

    // the time over which to achieve target speed
    float timeToTarget = 0.1f;  //0.1

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        Vector3 currentVelocity = character.linearVelocity;

        // If not moving, then there's no direction to look, so return null
        if (currentVelocity.magnitude == 0)
        {
            return null;
        }

        // If going somewhere, calculate the angle (direction) of that movement
        float direction = Mathf.Atan2(-currentVelocity.x, currentVelocity.z);
        direction *= Mathf.Rad2Deg;

        // Get the difference between the character's direction and it's current orientation
        float rotation = Vector3.SignedAngle(character.transform.forward, character.linearVelocity, Vector3.up); // degrees

        float rotationSize = Mathf.Abs(rotation);
        // check if we're close enough and if so don't rotate
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
            targetRotation = maxRotation * rotation / slowRadius;
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
