using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Restart() {
            Debug.Log("Restart game...");
            SceneManager.LoadScene("SceneNo2137"); //załadowanie sceny gry
        } 
    public void QuitGame() {
            Debug.Log("Quit game...");
            Application.Quit(); //wyjście z aplikacji
        }
}
