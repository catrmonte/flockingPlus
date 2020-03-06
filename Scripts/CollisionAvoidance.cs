using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : SteeringBehavior
{
    public Kinematic character;
    float maxAcceleration = 3f;

    public Kinematic[] targets;

    float radius = 3f; // our own collision radius

    public override SteeringOutput getSteering()
    {
        // Check for impending collisions
        float shortestTime = float.PositiveInfinity;

        Kinematic firstTarget = null; // first thing will collide with
        float firstMinSeperation = 1;
        float firstDistance = radius * 2;
        Vector3 firstRelativePosition = Vector3.zero;
        Vector3 firstRelativeVel = Vector3.zero;
        Vector3 relativePos;

        foreach (Kinematic target in targets)
        {
            // calculate the time to collision
            relativePos = target.transform.position - character.transform.position; // Hey does this need to be inverted? 
            //Vector3 relativeVel = target.linearVelocity - character.linearVelocity; // linear velocity from Kinematic

            //relativePos = character.transform.position - target.transform.position;
            Vector3 relativeVel = character.linearVelocity - target.linearVelocity;

            float relativeSpeed = relativeVel.magnitude;
            float timeToCollision = Vector3.Dot(relativePos, relativeVel ) / (relativeSpeed * relativeSpeed);

            // Will we be close enough at that time to care?
            float distance = relativePos.magnitude;
            float minSeperation = distance - relativeSpeed * timeToCollision;

            if (minSeperation > 2 * radius)
            {
                continue;
            }

            if (timeToCollision > 0 && timeToCollision < shortestTime)
            {
                // Store time and other data
                shortestTime = timeToCollision;
                firstTarget = target;
                firstMinSeperation = minSeperation;
                firstDistance = distance;
                firstRelativePosition = relativePos;
                firstRelativeVel = relativeVel;

                //Debug.Log("First min sep: " + firstMinSeperation + " and firstDist: " + firstDistance);
            }
        }

        //Debug.Log("First min sep: " + firstMinSeperation + " and firstDist: " + firstDistance);

        if (firstTarget == null)
        {
            //Debug.Log("No impending collisons");
            return null;
        }
        else if (firstMinSeperation <= 0 || firstDistance < 2 * radius)
        {
            //Debug.Log("Colliding");
            relativePos = firstTarget.transform.position - character.transform.position;
        }
        else
        {
            //Debug.Log("Collision imminent ");
            relativePos = firstRelativePosition + firstRelativeVel * shortestTime;
        }

        relativePos.Normalize();

        SteeringOutput result = new SteeringOutput();
        result.linear = relativePos * maxAcceleration;
        result.angular = 0;
        return result;
    }
}
