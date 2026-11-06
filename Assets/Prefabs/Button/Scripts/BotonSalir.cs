using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonSalir : MonoBehaviour
{
    // Start is called before the first frame update
    public void SalirJuego()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}
