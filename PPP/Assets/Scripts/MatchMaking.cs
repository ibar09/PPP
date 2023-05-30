using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class MatchMaking : MonoBehaviour
{
    public static MatchMaking Instance;
    public bool isOnlineGame = false;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void StartSoloRace()
    {

        SceneManager.LoadScene(1);
    }
    public void StartOnlineGame()
    {

        if (PhotonNetwork.IsMasterClient && PhotonNetwork.PlayerList.Length >= 2)
        {

            PhotonNetwork.LoadLevel(1);
        }
    }

}
