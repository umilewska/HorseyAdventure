using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Countdown : MonoBehaviour
{

    //[Header("Component")] public TextMeshProUGUI countdownDisplayOld;
    [Header("Component")] public Text countdownDisplay;
    private int CountdownTime = 3;
    private MovementPlayer _player;

    void Start()
    {
        StartCoroutine(CountdownToStart());
    }
    
    IEnumerator CountdownToStart()
    {
        while (CountdownTime > 0)
        {
            countdownDisplay.text = CountdownTime.ToString();

            yield return new WaitForSeconds(1f);

            CountdownTime--;
        }

        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownDisplay.gameObject.SetActive(false);
    }
}
