using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Component")] public TextMeshProUGUI timerText1;
    [Header("Component")] public TextMeshProUGUI timerText2;
    [Header("Component")] public TextMeshProUGUI timerText3;
    [Header("Component")] public Text level1TimeText;
    [Header("Component")] public Text level2TimeText;
    [Header("Component")] public Text level3TimeText;
    [Header("Component")] public Text totalTimeText;
    [Header("Component")] public Text bestTimeText;
    private float totalTime;
    private float level1Time;
    private float level2Time;
    private float level3Time;
    private float bestTime;

    private void Start()
    {
        level1Time = 0;
        level2Time = 0;
        level3Time = 0;
        totalTime = level1Time + level2Time + level3Time;
        timerText1.text = level1Time.ToString("0.00");
        timerText2.text = level2Time.ToString("0.00");
        timerText3.text = level3Time.ToString("0.00");
        level1TimeText.text = level1Time.ToString("0.00"); 
        level2TimeText.text = level2Time.ToString("0.00");
        level3TimeText.text = level3Time.ToString("0.00");
        totalTimeText.text = (level1Time + level2Time + level3Time).ToString("0.00");
        if (bestTimeText.text == "-")
        {
            bestTime = 1000f;
        }

        bestTimeText.text = PlayerPrefs.GetFloat("BestTime").ToString("0.00");
    }

    private void Update()
    {
        totalTime = PlayerPrefs.GetFloat("TotalTime");
        level1Time = PlayerPrefs.GetFloat("Level1Time");
        level2Time = PlayerPrefs.GetFloat("Level2Time");
        level3Time = PlayerPrefs.GetFloat("Level3Time");
        timerText1.text = level1Time.ToString("0.00");
        timerText2.text = level2Time.ToString("0.00");
        timerText3.text = level3Time.ToString("0.00");
        level1TimeText.text = level1Time.ToString("0.00"); 
        level2TimeText.text = level2Time.ToString("0.00");
        level3TimeText.text = level3Time.ToString("0.00");
        if (level1Time == 0)
        {
            level1TimeText.text = "-";
        }
        if (level2Time == 0)
        {
            level2TimeText.text = "-";
        }
        if (level3Time == 0)
        {
            level3TimeText.text = "-";
        }
        totalTimeText.text = (level1Time + level2Time + level3Time).ToString("0.00");
        if (level1Time + level2Time + level3Time == 0)
        {
            totalTimeText.text = "-";
        }
        bestTime = PlayerPrefs.GetFloat("BestTime");
        if (PlayerPrefs.GetInt("FinishFinish") == 1)
        {
            if ((level1Time + level2Time + level3Time) < bestTime)
            {
                Debug.Log(PlayerPrefs.GetFloat("BestTime"));
                PlayerPrefs.SetFloat("BestTime", level1Time + level2Time + level3Time);
                bestTime = PlayerPrefs.GetFloat("BestTime");
                bestTimeText.text = bestTime.ToString("0.00");
            }
            else
            {
                Debug.Log("DUPA");
                bestTimeText.text = PlayerPrefs.GetFloat("BestTime").ToString("0.00");
            }
        }
        Debug.Log(bestTime);
        //bestTimeText.text = (level1Time + level2Time).ToString("0.00");
    }

    public void Restart() 
    {
        Debug.Log("Restart game...");
        SceneManager.LoadScene("Level1"); //załadowanie sceny gry
    } 
    public void QuitGame() 
    {
        Debug.Log("Quit game...");
        Application.Quit(); //wyjście z aplikacji
    }
}
