using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogoCine : MonoBehaviour
{
    public TextMeshProUGUI preguntaText;
    public Button[] opciones;
    public string pregunta;
    public string[] respuestas;
    public int respuestaCorrecta;

    void Start()
    {
        MostrarPregunta();
    }

    void MostrarPregunta()
    {
        preguntaText.text = pregunta;

        for (int i = 0; i < opciones.Length; i++)
        {
            int index = i;
            opciones[i].GetComponentInChildren<TextMeshProUGUI>().text = respuestas[i];
            opciones[i].onClick.RemoveAllListeners();
            opciones[i].onClick.AddListener(() => VerificarRespuesta(index));
        }
    }

    void VerificarRespuesta(int seleccion)
    {
        if (seleccion == respuestaCorrecta)
        {
            Debug.Log("¡Correcto!");
            ChargeEscene();
        }
        else
        {
            Debug.Log("Incorrecto. Intentalo de nuevo.");
        }
    }
    void ChargeEscene()
    {
        SceneManager.LoadScene("Final");
    }
}
