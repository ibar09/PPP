using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private WheelCollider FrontRWheel;
    [SerializeField] private WheelCollider BackRWheel;
    [SerializeField] private WheelCollider FrontLWheel;
    [SerializeField] private WheelCollider BackLWheel;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxSteeringAngle;
    private float speed;
    private bool isBreaking;
    private float currentBreakStrength;
    [SerializeField] private float breakStrength;
    private float steeringAngle;


    private void FixedUpdate()
    {
        speed = maxSpeed * Input.GetAxis("Vertical");
        steeringAngle = maxSteeringAngle * Input.GetAxis("Horizontal");
        Debug.Log(speed);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentBreakStrength = breakStrength;
        }
        else
            currentBreakStrength = 0f;


        FrontLWheel.motorTorque = speed;
        FrontRWheel.motorTorque = speed;

        FrontLWheel.brakeTorque = currentBreakStrength;
        FrontRWheel.brakeTorque = currentBreakStrength;
        BackLWheel.brakeTorque = currentBreakStrength;
        BackRWheel.brakeTorque = currentBreakStrength;

        FrontLWheel.steerAngle = steeringAngle;
        FrontRWheel.steerAngle = steeringAngle;





    }



}
