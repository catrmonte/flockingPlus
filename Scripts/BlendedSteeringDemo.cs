using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BlendedSteeringDemo : MonoBehaviour
{
    public BehaviorAndWeight[] behaviors;// = new BehaviorAndWeight[3];

    public float maxAcceleration = 1f;
    float maxRotation = 5f;
    float weight = 0f;

        public SteeringOutput getSteering()
        {
            SteeringOutput result = new SteeringOutput();

            foreach (BehaviorAndWeight b in behaviors)
            {
            // Debug.Log(b.behavior);
            SteeringOutput s = b.behavior.getSteering();

                if (s != null)
                {
                    result.angular += s.angular * b.weight;
                    result.linear += s.linear * b.weight;
                }
            }

            // crop the result... maybe only do this if result.linear.magnitude > maxAccel
            // result.linear = result.linear.normalized * maxAcceleration;
            result.linear = result.linear.normalized * Mathf.Min(maxAcceleration, result.linear.magnitude);
            float angularAcc = Mathf.Abs(result.angular);
            if (angularAcc > maxRotation)
            {
                result.angular /= angularAcc;
                result.angular *= maxRotation;
            }

            return result;
        }
}
