using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControladorJuego : MonoBehaviour
{
    public int totalBasura = 5; // Cambia este número según cuántos objetos haya
    private int basuraRecogida = 0;

    public GameObject panelFinal; // Referencia al mensaje final
    public GameObject textoInstruccion;

    void Start()
    {
        if (panelFinal != null)
            panelFinal.SetActive(false); // Oculta el mensaje al inicio
        if (textoInstruccion != null)
            textoInstruccion.SetActive(true);
    }

    public void BasuraEliminada()
    {
        basuraRecogida++;

        if (basuraRecogida >= totalBasura)
        {
            MostrarMensajeFinal();
        }
    }

    private void MostrarMensajeFinal()
    {
        if (panelFinal != null)
            panelFinal.SetActive(true);
        if (textoInstruccion != null)
            textoInstruccion.SetActive(false);
    }
}