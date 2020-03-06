using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : Seek
{
    public float maxPrediction = 100f;
    float predictionTime;

    public override SteeringOutput getSteering()
    {
        Vector3 direction = target.transform.position - character.transform.position;
        float distance = direction.magnitude;

        // Speed is character's current velocity in a float
        float speed = character.linearVelocity.magnitude;

        if (speed <= (distance / maxPrediction))
        {
            // predictionTime is maximum
            predictionTime = maxPrediction;
        }
        else
        {
            // Slow predicition time to minimize overshooting
            predictionTime = distance / speed;
        }

        target.transform.position += target.linearVelocity * predictionTime;
        
        // Call base function
        return base.getSteering();
        
    }
}
