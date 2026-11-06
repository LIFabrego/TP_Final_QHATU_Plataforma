using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluacionFinal : MonoBehaviour
{
    public static EvaluacionFinal Instancia;

    public List<EvaluacionPregunta> respuestasJugador = new List<EvaluacionPregunta>();

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void RegistrarRespuesta(string pregunta, string respuestaJugador, string respuestaCorrecta)
    {
        bool esCorrecta = respuestaJugador == respuestaCorrecta;

        respuestasJugador.Add(new EvaluacionPregunta
        {
            pregunta = pregunta,
            respuestaJugador = respuestaJugador,
            respuestaCorrecta = respuestaCorrecta,
            esCorrecta = esCorrecta
        });
    }

}
