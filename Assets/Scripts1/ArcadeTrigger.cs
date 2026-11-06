using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeTrigger : MonoBehaviour
{
    public GameObject minigameMenuUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            minigameMenuUI.SetActive(true);
            Time.timeScale = 0f; // pausa el juego si quieres que el jugador no se mueva
        }
    }
}
