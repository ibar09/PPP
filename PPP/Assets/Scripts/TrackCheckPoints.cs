using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckPoints : MonoBehaviour
{
    public event EventHandler<CarCheckpointEventArgs> OnPlayerThrough;
    public class CarCheckpointEventArgs : EventArgs{
        public Transform carTransform;

    }
    public CheckpointSingle nextCheck ;
    // Start is called before the first frame update
    [SerializeField] private List<CheckpointSingle> checkpointSingleList;
    private void Awake() {
        Transform checkpointsTransform = transform.Find("Checkpoints");
        checkpointSingleList = new List<CheckpointSingle>();
        foreach (Transform checkpointSingleTransform in checkpointsTransform){
            CheckpointSingle  checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
            checkpointSingle.SetTrackCheckpoints(this);
            checkpointSingleList.Add(checkpointSingle);

        }
        nextCheck=checkpointSingleList[0];
    }
    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle,Transform t) {
        Debug.Log(checkpointSingle.transform.name);
        OnPlayerThrough?.Invoke(this, new CarCheckpointEventArgs{
            carTransform = t

        });
        Debug.Log(checkpointSingleList[checkpointSingleList.IndexOf(nextCheck)]);
    }
   
    public void GetNextCheckPoint(){
        if(checkpointSingleList.IndexOf(nextCheck)+1 <= checkpointSingleList.Count) {
        nextCheck=checkpointSingleList[checkpointSingleList.IndexOf(nextCheck)+1];
        }
        
    }
    
}
