using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{

    private TrackCheckPoints trackcheckpoints;
    private void OnTriggerEnter(Collider other) {
        Debug.Log("abc");
        if (other.TryGetComponent<Car>(out Car car)) {
           trackcheckpoints.PlayerThroughCheckpoint(this,other.transform);
        }

        
    }
    public void SetTrackCheckpoints(TrackCheckPoints trackCheckPoints) {
        this.trackcheckpoints = trackCheckPoints;
    }
}
