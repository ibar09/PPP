using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.transform.name == "Car")
        {
            Debug.Log(other.gameObject.transform.name);
        }
    }
}

