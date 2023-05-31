using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


public class DriveAgentAi : Agent {
    [SerializeField] private TrackCheckPoints trackCheckPoints;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private CarCollider carCollider;
    private int nextCheck;
    
    private CarController carController;
    private float distanceToCheckPoint;
    [SerializeField] private Transform objectiff;
    private float totaldistance;
    private   void Awake() {
        carController = GetComponent<CarController>();
    }
    private void Start() {
        trackCheckPoints.OnPlayerThrough+=trackCheck_onplayerthrough;
    }

   

    private void trackCheck_onplayerthrough(object sender, TrackCheckPoints.CarCheckpointEventArgs e) {
        
        if(e.carTransform == transform) {
            Debug.Log("here");
            AddReward(+20f);
            rewarddistance();
            trackCheckPoints.GetNextCheckPoint();
        }
    }
    public override void OnEpisodeBegin()
    {
        transform.localPosition=spawnPosition.localPosition + new Vector3(Random.Range(-5f,+5f),0,0);
        transform.forward = spawnPosition.forward;
        distanceToCheckPoint = Vector3.Distance(transform.localPosition,trackCheckPoints.nextCheck.transform.localPosition);
        totaldistance=Vector3.Distance(transform.localPosition,objectiff.position);

    }
    public void rewardnow(){
        float speed = GetComponent<Rigidbody>().velocity.magnitude;

        AddReward(0.05f*speed);
        
        //AddReward(0.05f*carController.gasInput);
        

    }
    public void rewarddistance(){
        float distance = Vector3.Distance(transform.localPosition,objectiff.position);
        // AddReward(-(distance*10f)/totaldistance);
        AddReward((totaldistance/distance)*20f);
       
            }
    private void Update() {
        InvokeRepeating("rewardnow",1f,1f);
        InvokeRepeating("rewarddistance",4f,6f);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
         Vector3 vectorobs= trackCheckPoints.nextCheck.transform.forward;
         float directionDot = Vector3.Dot(transform.forward,vectorobs);
        sensor.AddObservation(directionDot);
    }
    public override void OnActionReceived(ActionBuffers actions) {
       float forwarAmount = 0f;
       float turnAmount = 0f;

       switch (actions.DiscreteActions[0]) {
        case 0: forwarAmount = 0f; break;
        case 1: forwarAmount = +1f; break;
        case 2: forwarAmount = -1f; break;
       }
        switch (actions.DiscreteActions[1]) {
        case 0: turnAmount = 0f; break;
        case 1: turnAmount = +1f; break;
        case 2: turnAmount = -1f; break;
       }
        carController.SetInputs(forwarAmount, turnAmount);
    }
    private void  OnCollisionEnter(Collision collision) {
    
    if (collision.gameObject.TryGetComponent<Wall>(out Wall wall)) {
        Debug.Log("-30");
       AddReward(-30f);
    }
}
   




}
