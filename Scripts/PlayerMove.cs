using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Transform playerTransform;
    public float rotationIncrement = 3f;
    public float speed = 3f;
    Vector3 v;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d"))
        {
            v = new Vector3(0, rotationIncrement, 0);
            this.transform.eulerAngles += v * Time.deltaTime;
        }

        if (Input.GetKey("a"))
        {
            v = new Vector3(0, rotationIncrement, 0);
            this.transform.eulerAngles -= v * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            v = new Vector3(speed, 0, 0);
            this.transform.position -= v * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            v = new Vector3(speed, 0, 0);
            this.transform.position += v * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            v = new Vector3(0, 0, speed);
            this.transform.position -= v * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            v = new Vector3(0, 0, speed);
            this.transform.position += v * Time.deltaTime;
        }
    }
}
