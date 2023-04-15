using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Rigidbody car;
    [SerializeField] private TextMeshProUGUI speedText;
    void Update()
    {
        speedText.text = "Speed : " + Mathf.RoundToInt((float)(car.velocity.magnitude * 3.6)).ToString() + " Km/H";
    }
}
