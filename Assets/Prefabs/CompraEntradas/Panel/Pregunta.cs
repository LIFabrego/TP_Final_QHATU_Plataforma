using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Pregunta
{
    public string texto;
    public string[] opciones;
    public Sprite[] pictogramasOpciones;
    public int respuestaCorrecta;
    public int[] respuestasParciales;
    public AudioClip audioNarracion;
    public string textoAyuda;
    public AudioClip audioAyuda;    
}
