using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MensajeMiniJuego : MonoBehaviour
{
    public GameObject mensajeCompletado;

    void Start()
    {
        if (FichaManager.instance != null && FichaManager.instance.minijuegoCompletado)
        {
            mensajeCompletado.SetActive(true);
            // Ocultar el mensaje después de unos segundos
            Invoke("OcultarMensaje", 5f);

            // Reiniciar la bandera para la próxima vez
            FichaManager.instance.minijuegoCompletado = false;
        }
    }

    void OcultarMensaje()
    {
        mensajeCompletado.SetActive(false);
    }
}
