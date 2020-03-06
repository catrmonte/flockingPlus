using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Seperate dev

public class Arrive : SteeringBehavior
{
    public Kinematic character;
    public GameObject target;

    float maxAcceleration = 25f;
    float maxSpeed = 15f;
    public float targetRadius = 3f; // Has arrived radius
    float slowRadius = 15f;
    float timeToTarget = .1f;
    float targetSpeed;
    Vector3 targetVelocity;

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        // Direction towards target
        result.linear = target.transform.position - character.transform.position;

        // If we're there, stop
        if (result.linear.magnitude < targetRadius)
        {
            return null;
        }

        // If within the slow radius
        if (result.linear.magnitude > slowRadius)
        {
            targetSpeed = maxSpeed;
        }
        else
        {
            targetSpeed = maxSpeed * result.linear.magnitude / slowRadius;
        }

        // Combine the target speed and direction 
        targetVelocity = result.linear.normalized;
        targetVelocity *= targetSpeed;

        result.linear = targetVelocity - character.linearVelocity;
        result.linear /= timeToTarget;

        // Check yourself before you wreck yourself (speed)
        if (result.linear.magnitude > maxAcceleration)
        {
            result.linear = result.linear.normalized * maxAcceleration;
        }

        result.angular = 0;

        return result;
    }
}
