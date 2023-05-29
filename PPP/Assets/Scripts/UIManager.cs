using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Rigidbody car;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] public TextMeshProUGUI counterText;
    [SerializeField] private GameObject finishWindow;
    [SerializeField] private GameObject GUI;
    [SerializeField] private float currentValue = 3;
    public Image abilitySprite;
    public TextMeshProUGUI rankText;
    void Update()
    {
        speedText.text = Mathf.RoundToInt((float)(car.velocity.magnitude * 3.6)).ToString();
    }
    public void ShowFinishWindow()
    {
        GUI.SetActive(false);
        finishWindow.SetActive(true);
    }


}
