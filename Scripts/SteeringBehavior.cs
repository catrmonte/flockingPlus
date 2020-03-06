using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehavior : MonoBehaviour
{
    public abstract SteeringOutput getSteering();
}

public class BehaviorAndWeight
{
    public SteeringBehavior behavior;
    public float weight;
}
