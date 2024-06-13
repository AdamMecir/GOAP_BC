using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    //unity controls and constants input
    [SerializeField] public float accelerationMod;
    [SerializeField] public float xAxisSensitivity;
    [SerializeField] public float yAxisSensitivity;
    [SerializeField] public float deccelerationMod;
    [SerializeField] public string forwards;
    [SerializeField] public string backwards;
    [SerializeField] public string left;
    [SerializeField] public string right;
    [SerializeField] public string up;
    [SerializeField] public string down;

    private Vector3 moveSpeed;
    private bool xVector;
    private bool yVector;
    private bool zVector;

    void Start()
    {
        moveSpeed = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(moveSpeed);
        //was this axis moved on?
        xVector = false;
        yVector = false;
        zVector = false;
        //acceleration this iteration
        Vector3 acceleration = new Vector3();

        //mouse input
        float rotationHorizontal = xAxisSensitivity * Input.GetAxis("Mouse X");
        float rotationVertical = yAxisSensitivity * Input.GetAxis("Mouse Y");

        //applying mouse rotation
        transform.localEulerAngles = transform.localEulerAngles + new Vector3(-rotationVertical, rotationHorizontal, 0);

        //key input detection
        if (Input.GetKey(forwards))
        {
            //Debug.Log(forwards);
            xVector = true;
            acceleration += transform.forward;
            Debug.Log(acceleration);
        }

        if (Input.GetKey(left))
        {
            //Debug.Log(left);
            zVector = true;
            acceleration += -transform.right;
        }

        if (Input.GetKey(backwards))
        {
            //Debug.Log(backwards);
            xVector = true;
            acceleration += -transform.forward;
        }

        if (Input.GetKey(right))
        {
            //Debug.Log(right);
            zVector = true;
            acceleration += transform.right;
        }

        if (Input.GetKey(up))
        {
            //Debug.Log(up);
            yVector = true;
            acceleration += transform.up;
        }

        if (Input.GetKey(down))
        {
            //Debug.Log(down);
            yVector = true;
            acceleration += -transform.up;
        }

        //decceleration functionality
        if (!xVector)
        {
            //Debug.Log("xVector");
            if (Math.Abs(moveSpeed.x) < deccelerationMod)
            {
                moveSpeed = new Vector3(0, moveSpeed.y, moveSpeed.z);
            }
            else
            {
                moveSpeed = new Vector3(moveSpeed.x - deccelerationMod * Math.Sign(moveSpeed.x), moveSpeed.y, moveSpeed.z);
            }
        }

        if (!yVector)
        {
            //Debug.Log("yVector");
            if (Math.Abs(moveSpeed.y) < deccelerationMod)
            {
                moveSpeed = new Vector3(moveSpeed.x, 0, moveSpeed.z);
            }
            else
            {
                moveSpeed = new Vector3(moveSpeed.x, moveSpeed.y - deccelerationMod * Math.Sign(moveSpeed.y), moveSpeed.z);
            }
        }

        if (!zVector)
        {
            //Debug.Log("zVector");
            if (Math.Abs(moveSpeed.z) < deccelerationMod)
            {
                moveSpeed = new Vector3(moveSpeed.x, moveSpeed.y, 0);
            }
            else
            {
                moveSpeed = new Vector3(moveSpeed.x, moveSpeed.y, moveSpeed.z - deccelerationMod * Math.Sign(moveSpeed.z));
            }
        }


        Debug.Log(acceleration);
        //processing acceleration and applying it to movementSpeed/Velocity.
        acceleration = transform.TransformVector(acceleration);
        acceleration.Normalize();
        acceleration *= accelerationMod;
        moveSpeed += acceleration;
        Debug.Log(moveSpeed);
        //applying movementSpeed/Velocity
        transform.Translate(moveSpeed);
    }
}
