using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seperate : SteeringBehavior
{
    public Kinematic character;
    public float maxAcceleration = 50f;
    public float threshold = 5f;
    public float decayCoeffecient = 30f;

    public Kinematic[] targets;

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        // Loop through each target
        foreach (Kinematic target in targets)
        {
            // Check if target is close
            Vector3 direction = target.transform.position - character.transform.position;
            float distance = direction.magnitude;

            if (distance < threshold)
            {
                // Calculate strength of repulsion w/ inverse square law
                float strength = Mathf.Min(decayCoeffecient / (distance * distance),
                                           maxAcceleration);
                
                // Add acceleration
                result.linear -= strength * direction.normalized;
            }
        }

        return result;
    }
}
