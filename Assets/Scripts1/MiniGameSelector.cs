using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGameSelector : MonoBehaviour
{
    public GameObject menuUI;
    public Button[] botonesMinijuegos;

    private void Start()
    {
        if (FichaManager.instance.ObtenerFichas() <= 0)
        {
            foreach (Button b in botonesMinijuegos)
            {
                b.interactable = false;
            }
        }
    }

    public void CargarMinijuego(string nombreEscena)
    {
        if (FichaManager.instance.UsarFicha())
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nombreEscena);
        }
    }
}
