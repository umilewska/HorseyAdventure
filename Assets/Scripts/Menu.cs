using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    private static Timer timer;
    [Header("Component")]
    public TextMeshProUGUI timerText;

    private void Update()
    {
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
