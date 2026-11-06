using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EvaluacionUI : MonoBehaviour
{
    public Transform contenedorRespuestas;
    public GameObject prefabRespuesta;
    public TextMeshProUGUI textoResumen;

    private void Start()
    {
        MostrarEvaluacion();
    }
    void MostrarEvaluacion()
    {
        var evaluacion = EvaluacionFinal.Instancia;

        foreach (var r in evaluacion.respuestasJugador)
        {
            GameObject item = Instantiate(prefabRespuesta, contenedorRespuestas);
            item.transform.Find("PreguntaText").GetComponent<TextMeshProUGUI>().text = r.pregunta;
            item.transform.Find("RespuestaJugadorText").GetComponent<TextMeshProUGUI>().text = "Tu respuesta: " + r.respuestaJugador;
            item.transform.Find("RespuestaCorrectaText").GetComponent<TextMeshProUGUI>().text = r.esCorrecta ? "Correcto" : "Incorrecta: " + "Respuesta Correcta:" + r.respuestaCorrecta;

            // Colorea según acierto
            var colorTexto = r.esCorrecta ? Color.green : Color.red;
            item.transform.Find("RespuestaJugadorText").GetComponent<TextMeshProUGUI>().color = colorTexto;
        }
    }
     public void VolverAlMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Final");
    }
}
