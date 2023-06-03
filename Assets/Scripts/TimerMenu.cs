using System;
using System.Collections;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerMenu : MonoBehaviour
{
    [SerializeField] public Text totalTimeText;
    private float totalTime;

    void Start()
    {
        totalTime = 0f;
        totalTimeText.text = totalTime.ToString("0.00");
    }

    private void Update()
    {
        totalTime = PlayerPrefs.GetFloat("TotalTime");
        totalTimeText.text = totalTime.ToString("0.00");
    }
}
