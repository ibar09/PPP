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
    [SerializeField] private Transform FrontRWheelT;
    [SerializeField] private Transform BackRWheelT;
    [SerializeField] private Transform FrontLWheelT;
    [SerializeField] private Transform BackLWheelT;
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
        if (Input.GetKey(KeyCode.Space))
        {
            currentBreakStrength = breakStrength;
        }
        else
            currentBreakStrength = 0f;


        FrontLWheel.motorTorque = speed;
        FrontRWheel.motorTorque = speed;

        if (FrontLWheel.rpm > 500)
            FrontLWheel.motorTorque = 0;

        if (FrontRWheel.rpm > 500)
            FrontRWheel.motorTorque = 0;

        FrontLWheel.brakeTorque = currentBreakStrength;
        FrontRWheel.brakeTorque = currentBreakStrength;
        BackLWheel.brakeTorque = currentBreakStrength;
        BackRWheel.brakeTorque = currentBreakStrength;

        FrontLWheel.steerAngle = steeringAngle;
        FrontRWheel.steerAngle = steeringAngle;



        UpdateSteeringWheel(FrontLWheel, FrontLWheelT);
        UpdateSteeringWheel(FrontRWheel, FrontRWheelT);
        UpdateSteeringWheel(BackLWheel, BackLWheelT);
        UpdateSteeringWheel(BackRWheel, BackRWheelT);

    }

    public void UpdateSteeringWheel(WheelCollider wheelCollider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }



}
