using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreguntaManager : MonoBehaviour
{


    [SerializeField] private Pregunta[] preguntas;
    private int preguntaActual = 0;
    public enum TipoRespuesta
    {
        Incorrecta,
        Parcial,
        Correcta
    }
    public Pregunta ObtenerPreguntaActual()
    {
        if (preguntaActual < preguntas.Length)
            return preguntas[preguntaActual];

        return null;
    }

    /*public bool VerificarRespuesta(int seleccion)
    {
        if (preguntaActual < preguntas.Length)
        {
            return seleccion == preguntas[preguntaActual].respuestaCorrecta;
        }
        return false;
    }*/
    public TipoRespuesta VerificarRespuesta(int seleccion)
    {
        Pregunta pregunta = ObtenerPreguntaActual();

        if (pregunta == null)
            return TipoRespuesta.Incorrecta;

        if (seleccion == pregunta.respuestaCorrecta)
            return TipoRespuesta.Correcta;

        if (pregunta.respuestasParciales != null && pregunta.respuestasParciales.Contains(seleccion))
            return TipoRespuesta.Parcial;

        return TipoRespuesta.Incorrecta;
    }
    public bool AvanzarPregunta()
    {
        preguntaActual++;
        return preguntaActual < preguntas.Length;
    }

    public bool FinDelCuestionario()
    {
        return preguntaActual >= preguntas.Length;
    }
}