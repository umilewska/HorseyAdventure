using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    private Timer timer = new Timer();
    [Header("Component")]
    public TextMeshProUGUI timerText;

    private void Start()
    {
        //timerText.text = "0";
        timerText.text = timer.getFinalTime().ToString();   //nie działa Xddd
    }
    
    public void Restart() {
            Debug.Log("Restart game...");
            SceneManager.LoadScene("SceneNo2137"); //załadowanie sceny gry
        } 
    public void QuitGame() {
            Debug.Log("Quit game...");
            Application.Quit(); //wyjście z aplikacji
        }
}
