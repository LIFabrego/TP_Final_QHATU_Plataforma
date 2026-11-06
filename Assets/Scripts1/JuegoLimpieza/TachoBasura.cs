using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TachoBasura : MonoBehaviour
{
    public ControladorJuego controlador;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Basura"))
        {
            Destroy(other.gameObject);

            if (controlador != null)
            {
                controlador.BasuraEliminada();
            }
        }
    }
}