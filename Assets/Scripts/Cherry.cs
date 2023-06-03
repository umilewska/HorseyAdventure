using UnityEngine;

public class Cherry : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Sprawdź, czy kolizja dotyczy obiektu gracza
        if (col.gameObject.CompareTag("Player"))
        {
            // Znikanie obiektu-triggera
            gameObject.SetActive(false);
        }
    }
}
