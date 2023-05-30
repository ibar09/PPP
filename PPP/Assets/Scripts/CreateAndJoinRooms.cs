using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    [SerializeField] private TMP_InputField createRoom, joinRoom;
    [SerializeField] private GameObject roomCreation, Lobby, list;

    [SerializeField] private GameObject playerNamePrefab;
    [SerializeField] private List<GameObject> players;

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoom.text);
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoom.text, new Photon.Realtime.RoomOptions { MaxPlayers = 4 });

    }

    public override void OnJoinedRoom()
    {
        UpdatePlayerList();
        MatchMaking.Instance.isOnlineGame = true;
        roomCreation.SetActive(false);
        Lobby.SetActive(true);
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        UpdatePlayerList();

        // Update your UI or perform any other actions with the updated player list
    }

    void UpdatePlayerList()
    {
        foreach (GameObject player in players)
        {
            Destroy(player);
        }
        if (PhotonNetwork.CurrentRoom == null)
            return;
        foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            string playerName = "Player" + player.Key.ToString();
            Debug.Log(playerName);
            GameObject x = Instantiate(playerNamePrefab, list.transform);
            players.Add(x);
            x.GetComponent<TextMeshProUGUI>().text = playerName;
            Debug.Log("New player joined: " + playerName);
        }

    }
}
