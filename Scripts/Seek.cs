using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehavior
{
    public Kinematic character;
    public Kinematic target;

    float maxAcceleration;

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        // Get the direction to the target
        result.linear = target.transform.position - character.transform.position;

        return result;
    }
}
