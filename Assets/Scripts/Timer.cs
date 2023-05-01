using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")] 
    public float currentTime;
    public bool countDown;
    
    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    [Header("Format Settings")] 
    public TimerFormats format;
    public Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    private MovementPlayer _player;
    private float finalTime;


    // Start is called before the first frame update
    void Start()
    {
        timerText.text = "0";
        timeFormats.Add(TimerFormats.Whole, "0");   //odliczanie całościowe do startu 
        timeFormats.Add(TimerFormats.HundrethsDecimal, "0.00");     // mierzenie czasu 
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

        // Odliczanie przed startem wyścigu - jescze nie zrobione
        if (hasLimit && (!countDown && currentTime >= timerLimit))
        {
            currentTime = timerLimit;
            SetTimerText();
            enabled = false;
        }

        if (!hasLimit && _player.HasFinished())
        {
            enabled = false;
            finalTime = currentTime;
            SetTimerText();
        }
        SetTimerText();
    }

    private void SetTimerText()
    {
        timerText.text = currentTime.ToString(timeFormats[format]);
    }

    public float getFinalTime()
    {
        return finalTime;
    }
}

public enum TimerFormats
{
    Whole,
    HundrethsDecimal
}
