using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Seperate dev

public class Kinematic : MonoBehaviour
{
    public Vector3 linearVelocity;
    public float angularVelocity;  // Millington calls this rotation
    // because I'm attached to a gameobject, we also have:
    // rotation <<< Millington calls this orientation
    // position
    public int behaviorNum = 1;
    public Text text;

    public GameObject myTarget;
    public GameObject myTargetObstacle;
    public Kinematic alsoMyTargetForPursueTho;

    pathFollow myPathFollow = new pathFollow();
    Seperate mySeperate = new Seperate();

    // Set of gameobjects to create path
    public GameObject[] pathToFollow;

    // Set of targets to seperate away from
    public Kinematic[] targetsForSeperate;

    // Priority Steering
    PrioritySteering myPriority = new PrioritySteering();

    // Whether object is avoiding obstacles for Priority Steering
    public bool avoiding = false;
    public bool setUp = false;

    BlendedSteeringDemo mySteering;
    PrioritySteering myAdvancedSteering;
    Kinematic[] kBirds;

    public enum Behavior
    {
        Seek,
        Flee
    }

    // Update is called once per frame
    void Update()
    {
        if (behaviorNum != 0)
        {
            // update my position and rotation
            this.transform.position += linearVelocity * Time.deltaTime;
            Vector3 v = new Vector3(0, angularVelocity, 0);
            this.transform.eulerAngles += v * Time.deltaTime;
        }

        // update linear and angular velocities
        SteeringOutput steering = new SteeringOutput();

        // Seek: target is "alsoMyTargetForPursueTho"
        if (behaviorNum == 1)
        {
            text.text = "Seek";

            Seek mySeek = new Seek();
            mySeek.target = alsoMyTargetForPursueTho;
            mySeek.character = this;

            steering = mySeek.getSteering();
            linearVelocity += steering.linear * Time.deltaTime;
            angularVelocity += steering.angular * Time.deltaTime;
        }
        // Flee
        else if (behaviorNum == 2)
        {
            Flee myFlee = new Flee();
            text.text = "Flee";

            myFlee.character = this;
            myFlee.target = myTarget;
            steering = myFlee.getSteering();
            linearVelocity += steering.linear * Time.deltaTime;
            angularVelocity += steering.angular * Time.deltaTime;
        }
        // Arrive
        else if (behaviorNum == 3)
        {
            Arrive myArrive = new Arrive();
            text.text = "Arrive";

            myArrive.character = this;
            myArrive.target = myTarget;
            steering = myArrive.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
            else
            {
                linearVelocity = Vector3.zero;
            }
        }
        // Align
        else if (behaviorNum == 4)
        {
            Align myAlign = new Align();
            text.text = "Align";

            myAlign.character = this;
            myAlign.target = myTarget;
            steering = myAlign.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Face
        else if (behaviorNum == 5)
        {
            Face myFace = new Face();
            text.text = "Face";

            myFace.character = this;
            myFace.target = myTarget;
            steering = myFace.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Look where you're going
        else if (behaviorNum == 6)
        {
            LWYG myLook = new LWYG();
            text.text = "LWYG";

            myLook.character = this;
            steering = myLook.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Path following
        else if (behaviorNum == 7)
        {
            text.text = "Path Follow";

            myPathFollow.character = this;
            myPathFollow.path = pathToFollow;
            steering = myPathFollow.getSteering();
            if (steering != null)
            {
                if (linearVelocity.magnitude > 2)
                {
                    linearVelocity = linearVelocity.normalized * 2;
                }
                
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Pursue
        else if (behaviorNum == 8)
        {
            Pursue myPursue = new Pursue();
            text.text = "Pursue";

            myPursue.character = this;
            myPursue.target = alsoMyTargetForPursueTho;
            steering = myPursue.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Seperate
        else if (behaviorNum == 9)
        {
            text.text = "Seperate";

            mySeperate.character = this;
            mySeperate.targets = targetsForSeperate;
            steering = mySeperate.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Collision Avoidance
        else if (behaviorNum == 10)
        {
            text.text = "Collision Avoidance";

            CollisionAvoidance myCollision = new CollisionAvoidance();
            myCollision.character = this;
            myCollision.targets = targetsForSeperate;
            steering = myCollision.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Obstacle Avoidance
        else if (behaviorNum == 11)
        {
            text.text = "Obstacle Avoidance";

            ObstacleAvoid myObstacleAvoid = new ObstacleAvoid();
            myObstacleAvoid.character = this;
            myObstacleAvoid.target = myTarget;
            steering = myObstacleAvoid.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Flocking
        else if (behaviorNum == 12)
        {
            text.text = "Flocking";

            if (!setUp)
            {
                // BehaviorAndWeight1 : Seperate
                BehaviorAndWeight behavior1 = new BehaviorAndWeight();
                Seperate mySeperate = new Seperate();
                mySeperate.character = this;
                GameObject[] birds = GameObject.FindGameObjectsWithTag("Bird");
                Kinematic[] kBirds = new Kinematic[birds.Length - 1];

                int j = 0;
                for (int i = 0; i < birds.Length - 1; i++)
                {
                    if (birds[i] == this)
                    {
                        continue;
                    }
                    kBirds[j++] = birds[i].GetComponent<Kinematic>();
                }
                mySeperate.targets = kBirds;

                behavior1.behavior = mySeperate;
                behavior1.weight = 10f;

                // BehaviorAndWeight2 : Arrive
                BehaviorAndWeight behavior2 = new BehaviorAndWeight();
                Arrive myArrive = new Arrive();
                myArrive.character = this;
                myArrive.target = myTarget;
                behavior2.behavior = myArrive;
                behavior2.weight = 10f;

                // BehaviorAndWeight3 : Align
                BehaviorAndWeight behavior3 = new BehaviorAndWeight();
                Align myAlign = new Align();
                myAlign.character = this;
                myAlign.target = myTarget;
                behavior3.behavior = myAlign;
                behavior3.weight = 3f;

                // BehaviorAndWeight4 : ObstacleAvoidance
                BehaviorAndWeight behavior4 = new BehaviorAndWeight();
                ObstacleAvoid myObstacle = new ObstacleAvoid();
                myObstacle.character = this;
                myObstacle.target = myTargetObstacle; // Does this make sense? 
                behavior4.behavior = myObstacle;
                behavior4.weight = 1f;

                // Lower priority steering behaviors: Arrive, Align, & Seperate
                BlendedSteeringDemo myBlended = new BlendedSteeringDemo();
                myBlended.behaviors = new BehaviorAndWeight[3];
                myBlended.behaviors[0] = new BehaviorAndWeight();
                myBlended.behaviors[0] = behavior1;
                myBlended.behaviors[1] = new BehaviorAndWeight();
                myBlended.behaviors[1] = behavior2;
                myBlended.behaviors[2] = new BehaviorAndWeight();
                myBlended.behaviors[2] = behavior3;

                // Higher priority steering behavior: Obstacle avoidance
                BlendedSteeringDemo myBlendedAvoid = new BlendedSteeringDemo();
                myBlendedAvoid.behaviors = new BehaviorAndWeight[1];
                myBlendedAvoid.behaviors[0] = new BehaviorAndWeight();
                myBlendedAvoid.behaviors[0] = behavior4;

                // Initialize myPriority's array of groups with two groups
                myPriority.groups = new BlendedSteeringDemo[2];
                myPriority.groups[0] = new BlendedSteeringDemo();
                myPriority.groups[0] = myBlendedAvoid;
                myPriority.groups[1] = new BlendedSteeringDemo();
                myPriority.groups[1] = myBlended;

                setUp = true;
            }
            

            steering = myPriority.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        
    }

    // Cycles through behaviorNums to change what the behaviorCube is doing
    public void ChangeBehavior()
    {
        behaviorNum++;
        
        if (behaviorNum > 9)
        {
            behaviorNum = 1;
        }
    }
}
