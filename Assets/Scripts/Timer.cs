using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Component")] public TextMeshProUGUI timerText;
    [Header("Component")] public TextMeshProUGUI timeText;
    [Header("Component")] public Text timer;
    [Header("Component")] public Text time;
    [SerializeField] public int level;
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
        time.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
        yield return new WaitForSeconds(4f);
        time.gameObject.SetActive(true);
        timer.gameObject.SetActive(true);
        timer.text = "0";
        startTime = Time.time;
    }
    private void Update()
    {
        if (PlayerPrefs.GetInt("TimerStop") != 1)
        {
            currentTime = Time.time - startTime;
        }
        timer.text = currentTime.ToString("0.00");
        
        if (level == 1)
        {
            PlayerPrefs.SetFloat("Level1Time", currentTime);
        }

        if (level == 2)
        {
            PlayerPrefs.SetFloat("Level2Time", currentTime);
        }
        
        if (level == 3)
        {
            PlayerPrefs.SetFloat("Level3Time", currentTime);
        }
        
        // PlayerPrefs.SetFloat("TotalTime", currentTime);
        //
        // if (PlayerPrefs.GetInt("PlayerHasFinished") == 1)
        // {
        //     finalTime = currentTime; 
        //     timerText.text = finalTime.ToString("0.00");
        //     enabled = false;
        // }
    }
    
    public float getFinalTime()
    {
        return finalTime;
    }
    
}