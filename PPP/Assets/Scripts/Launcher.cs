using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Launcher : MonoBehaviourPunCallbacks
{

    public PhotonView playerPrefab; 
    public Vector3 vector = new Vector3((float)-111.79,(float) 17.45,(float) 84.82);

    // Start is called before the first frame update
    void Start()
    {
        //try to connect
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        //we connected
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room successfully");
        PhotonNetwork.Instantiate(playerPrefab.name, vector, Quaternion.identity);
    }
}
