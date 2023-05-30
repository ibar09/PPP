using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CarController : MonoBehaviour
{
    [SerializeField] private WheelCollider FrontRWheel;
    [SerializeField] private WheelCollider BackRWheel;
    [SerializeField] private WheelCollider FrontLWheel;
    [SerializeField] private WheelCollider BackLWheel;
    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private Transform FrontRWheelT;
    [SerializeField] private Transform BackRWheelT;
    [SerializeField] private Transform FrontLWheelT;
    [SerializeField] private Transform BackLWheelT;
    [SerializeField] private Transform gravityCenter;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxSpeed;
    private float motorTorque;
    private float speed;
    private float gasInput;
    public float brakeInput;
    public float steerInput;
    private float speedClamped;
    private float currentBreakStrength;
    [SerializeField] private float breakStrength;
    private float steeringAngle;
    public AnimationCurve steeringCurve;
    public AnimationCurve accelerationCurve;
    [SerializeField] private float forceStrength;
    public int isEngineRunning;
    [SerializeField] private PhotonView view;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = gravityCenter.localPosition;
        view = GetComponent<PhotonView>();
    }
    private void FixedUpdate()
    {
        if (MatchMaking.Instance.isOnlineGame)
        {

            if (view.IsMine)
            {
                Drive();
            }
        }
        else
        {
            Drive();
        }
    }
    private void Drive()
    {
        speed = rb.velocity.magnitude;
        if (Input.GetAxis("Vertical") != 0)
            SetInputs(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        float slipAngle = Vector3.Angle(transform.forward, rb.velocity - transform.forward);
        float movingDirection = Vector3.Dot(transform.forward, rb.velocity);
        float accelerationfactor = accelerationCurve.Evaluate(speed);
        speedClamped = Mathf.Lerp(speedClamped, speed, Time.deltaTime);
        rb.AddForce(-transform.forward * accelerationfactor * forceStrength, ForceMode.VelocityChange);
        if (Mathf.Abs(gasInput) > 0 && isEngineRunning == 0)
        {
            StartCoroutine(GetComponent<EngineAudio>().StartEngine());
        }
        if (movingDirection < -0.5f && gasInput > 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else if (movingDirection > 0.5f && gasInput < 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else
        {
            brakeInput = 0;
        }

        motorTorque = maxMotorTorque * gasInput;




        BackLWheel.motorTorque = motorTorque;
        BackRWheel.motorTorque = motorTorque;

        if (speed >= maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        ApplyBrake();
        steeringAngle = steeringCurve.Evaluate(speed) * steerInput;
        if (slipAngle < 120f)
        {
            steeringAngle += Vector3.SignedAngle(transform.forward, rb.velocity + transform.forward, Vector3.up);
        }
        steeringAngle = Mathf.Clamp(steeringAngle, -90f, 90f);
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
    void ApplyBrake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            FrontLWheel.brakeTorque = breakStrength * 0.7f;
            FrontRWheel.brakeTorque = breakStrength * 0.7f;
            BackLWheel.brakeTorque = breakStrength * 0.3f;
            BackRWheel.brakeTorque = breakStrength * 0.3f;
        }
        else
        {
            FrontLWheel.brakeTorque = brakeInput * breakStrength * 0.7f;
            FrontRWheel.brakeTorque = brakeInput * breakStrength * 0.7f;
            BackLWheel.brakeTorque = brakeInput * breakStrength * 0.3f;
            BackRWheel.brakeTorque = brakeInput * breakStrength * 0.3f;
        }


    }
    public float GetSpeedRatio()
    {
        var gas = Mathf.Clamp(Mathf.Abs(Input.GetAxis("Vertical")), 0.5f, 1f);
        return speedClamped * gas / maxSpeed;
    }

    public void SetInputs(float gasInput, float steerInput)
    {
        this.gasInput = gasInput;
        this.steerInput = steerInput;
    }

}
