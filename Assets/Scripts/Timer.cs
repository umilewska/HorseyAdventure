using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")] public TextMeshProUGUI timerText;
    [Header("Component")] public TextMeshProUGUI timeText;
    private float currentTime;
    protected static float finalTime;
    private MovementPlayer _player;
    private float startTime = 4f;

    
        // void Start()
    // {
    //     StartCoroutine(CountTime());
    // }

    // IEnumerator CountTime()
    // {
    //     yield return new WaitForSeconds(4f);
    //     currentTime += Time.deltaTime;
    //     timerText.text = currentTime.ToString("0.00");
    //     if (_player.HasFinished())
    //     {
    //         finalTime = currentTime;
    //         timerText.text = finalTime.ToString();
    //         enabled = false;
    //     }
    // }
    
    IEnumerator Start()
    {
        timeText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        yield return new WaitForSeconds(4f);
        timeText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        
    }
    private void Update()
    {
        currentTime = Time.time - startTime;
        timerText.text = currentTime.ToString("0.00");
        if (_player.HasFinished())
        {
            finalTime = currentTime; 
            timerText.text = finalTime.ToString(); 
            enabled = false;
        }
    }

    public float getFinalTime()
    {
        return finalTime;
    }
    
}