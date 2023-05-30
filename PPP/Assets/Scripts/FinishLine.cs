using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FinishLine : MonoBehaviour
{
    public List<CarController> cars;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Finished");
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                other.gameObject.GetComponent<CarController>().enabled = false;
                GameManager.Instance.UImanager.ShowFinishWindow();
                GameManager.Instance.UImanager.rankText.text = other.gameObject.GetComponent<Player>().rank.ToString();
                Debug.Log("Finished");
            }
        }
    }
    private void Update()
    {
        foreach (CarController car in cars)
        {
            car.enabled = false;
        }
    }
}
