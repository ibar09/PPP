using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject loading;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnConnectedToMaster()
    {

        menu.SetActive(true);
        loading.SetActive(false);
        Debug.Log("Connected");
    }
    public void JoinLobby()
    {
        FindObjectOfType<SlimUI.ModernMenu.MainMenuNew>().Position2();

        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {


        Debug.Log("joined lobby");
    }

}
