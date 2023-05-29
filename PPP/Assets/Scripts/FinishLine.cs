using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public List<CarController> cars;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Finished");
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CarController>().enabled = false;
            GameManager.Instance.UImanager.ShowFinishWindow();
            GameManager.Instance.UImanager.rankText.text = other.gameObject.GetComponent<Player>().rank.ToString();
            Debug.Log("Finished");
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
