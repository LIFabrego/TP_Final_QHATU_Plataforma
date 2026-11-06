using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorSonidos : MonoBehaviour
{
    [Header("Datos de los niveles")]
    public List<NivelSonido> niveles;

    [Header("UI")]
    public Button botonReproducir;
    public Button botonSiguiente;
    public Image[] opcionesImagenes;
    public GameObject feedbackCorrecto;
    public GameObject feedbackIncorrecto;

    [Header("Audio")]
    public AudioSource audioSource;

    private int nivelActual = 0;
    private int indiceCorrecto;

    [Header("Sonidos de Feedback")]
    public AudioClip sonidoAcierto;
    public AudioClip sonidoError;

    [SerializeField]
    private GameObject panelFinal;
    void Start()
    {
        botonReproducir.onClick.AddListener(ReproducirSonido);
        botonSiguiente.onClick.AddListener(PasarAlSiguienteNivel);
        MostrarNivel();
    }

    void MostrarNivel()
    {
        feedbackCorrecto.SetActive(false);
        feedbackIncorrecto.SetActive(false);
        botonSiguiente.interactable = false;

        NivelSonido nivel = niveles[nivelActual];

        // Creamos una lista local de distractores sin repetir la correcta
        List<Sprite> distractoresDisponibles = new List<Sprite>(nivel.imagenesDistractoras);

        // Asegurarse que no esté la correcta en los distractores
        distractoresDisponibles.Remove(nivel.imagenCorrecta);

        // Mezclar la lista (opcional pero recomendable)
        for (int i = 0; i < distractoresDisponibles.Count; i++)
        {
            Sprite temp = distractoresDisponibles[i];
            int randomIndex = Random.Range(i, distractoresDisponibles.Count);
            distractoresDisponibles[i] = distractoresDisponibles[randomIndex];
            distractoresDisponibles[randomIndex] = temp;
        }

        // Elegir una posición aleatoria para la imagen correcta
        indiceCorrecto = Random.Range(0, opcionesImagenes.Length);

        for (int i = 0, distractorIndex = 0; i < opcionesImagenes.Length; i++)
        {
            Button btn = opcionesImagenes[i].GetComponent<Button>();
            btn.onClick.RemoveAllListeners();

            if (i == indiceCorrecto)
            {
                opcionesImagenes[i].sprite = nivel.imagenCorrecta;
                btn.onClick.AddListener(() => RespuestaCorrecta());
            }
            else
            {
                if (distractorIndex < distractoresDisponibles.Count)
                {
                    opcionesImagenes[i].sprite = distractoresDisponibles[distractorIndex];
                    distractorIndex++;
                }
                btn.onClick.AddListener(() => RespuestaIncorrecta());
            }
        }
    }
    void ActivarOpciones(bool activar)
    {
        foreach (Image img in opcionesImagenes)
        {
            img.GetComponent<Button>().interactable = activar;
        }
    }
    void ReproducirSonido()
    {
        ActivarOpciones(false); // Desactiva los botones mientras suena
        audioSource.clip = niveles[nivelActual].sonido;
        audioSource.Play();
        StartCoroutine(EsperarFinDeAudio());
    }
    System.Collections.IEnumerator EsperarFinDeAudio()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        ActivarOpciones(true); // Ahora sí puede responder
    }

    void RespuestaCorrecta()
    {
        feedbackCorrecto.SetActive(true);
        feedbackIncorrecto.SetActive(false);
        botonSiguiente.interactable = true;
        if (sonidoAcierto != null)
        {
            audioSource.PlayOneShot(sonidoAcierto);
        }
    }

    void RespuestaIncorrecta()
    {
        feedbackIncorrecto.SetActive(true);
        feedbackCorrecto.SetActive(false);

        if (sonidoError != null)
        {
            audioSource.PlayOneShot(sonidoError);
        }
    }

    void PasarAlSiguienteNivel()
    {
        nivelActual++;

        if (nivelActual >= niveles.Count)
        {
            FinalizarJuego();
        }
        else
        {
            MostrarNivel();
        }
    }
    void FinalizarJuego()
    {
        Debug.Log("¡Juego finalizado!");
        panelFinal.SetActive(true); // Muestra mensaje final
        botonReproducir.gameObject.SetActive(false);
        botonSiguiente.gameObject.SetActive(false);

        foreach (Image img in opcionesImagenes)
        {
            img.gameObject.SetActive(false); 
        }

        feedbackCorrecto.SetActive(false);
        feedbackIncorrecto.SetActive(false);
    }
}
