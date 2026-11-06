using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PreguntaManager;

public class DialogoInteractivo : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private TextMeshProUGUI preguntaText;
    [SerializeField] private Button[] botonesRespuesta;
    [SerializeField] private Image[] pictogramas;

    [Header("Lógica")]
    [SerializeField] private PreguntaManager preguntaManager;

    [SerializeField] private Color colorCorrecto = Color.green;
    [SerializeField] private Color colorParcial = Color.yellow;
    [SerializeField] private Color colorIncorrecto = Color.red;

    [SerializeField] private Sprite spriteCorrecto;
    [SerializeField] private Sprite spriteParcial;
    [SerializeField] private Sprite spriteIncorrecto;
    [SerializeField] private Sprite spritePorDefecto;

    [SerializeField] private AudioClip sonidoCorrecto;
    [SerializeField] private AudioClip sonidoParcial;
    [SerializeField] private AudioClip sonidoIncorrecto;

    [SerializeField] private AudioSource audioUI;

    [SerializeField] private AudioSource narradorAudioSource;
    //Texto Ayuda
    [SerializeField] private GameObject cuadroDeAyuda;
    [SerializeField] private TextMeshProUGUI textoAyuda;
    [SerializeField] private Button botonVolver;
    [SerializeField] private Button botonAyuda;

    [SerializeField] private AudioSource ayudaAudioSource;

    // Para el tipeo
    [SerializeField] private float velocidadTipeo = 0.05f;
    [SerializeField] private float tiempoFeedback = 1.5f;
    //errores
    [Header("Sistema de errores")]
    [SerializeField] private Image[] indicadoresErrores; // 3 imágenes para mostrar errores
    [SerializeField] private Sprite spriteError; // Sprite que se muestra cuando hay un error
    [SerializeField] private string escenaDerrota = "YouLose";
    private int errores = 0;
    [SerializeField] private Sprite spritePorDefectoError;
    private const int erroresMaximos = 3;

    private void Start()
    {
        errores = 0;

        for (int i = 0; i < indicadoresErrores.Length; i++)
        {
            indicadoresErrores[i].sprite = spritePorDefectoError;
        }
        MostrarPregunta();
    }

    private void MostrarPregunta()
    {
        Pregunta pregunta = preguntaManager.ObtenerPreguntaActual();

        if (pregunta == null)
        {
            /*Debug.Log("sin preguntas");
            int escenaActual = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene("Loby");
            return;*/
            string escenaActual = SceneManager.GetActiveScene().name;

            switch (escenaActual)
            {
                case "CompraEntradas":
                    ObjetivosManager.Instance.MarcarCumplido("Comprar entradas");
                    SceneManager.LoadScene("Loby");
                    break;

                case "CompraComida":
                    ObjetivosManager.Instance.MarcarCumplido("Comprar comida");
                    SceneManager.LoadScene("Loby");
                    break;

                case "SalaCine":
                    ObjetivosManager.Instance.MarcarCumplido("Sala Cine");
                    SceneManager.LoadScene("InteriorSala");
                    break;

                default:
                    Debug.LogWarning("Escena actual no reconocida");
                    break;
            }

            return;
        }
        
        preguntaText.text = pregunta.texto;

        for (int i = 0; i < botonesRespuesta.Length; i++)
        {
            int index = i;
            Image pictogramaHijo = botonesRespuesta[i].transform.Find("PictogramaOpcion").GetComponent<Image>();
            if (pregunta.pictogramasOpciones != null && pregunta.pictogramasOpciones.Length > i)
            {
                pictogramaHijo.sprite = pregunta.pictogramasOpciones[i];
                pictogramaHijo.enabled = true;
            }
            else
            {
                pictogramaHijo.enabled = false;
            }
            botonesRespuesta[i].image.sprite = spritePorDefecto;
            botonesRespuesta[i].interactable = true;
            botonesRespuesta[i].image.color = Color.white;
            botonesRespuesta[i].GetComponentInChildren<TextMeshProUGUI>().text = pregunta.opciones[i];
            botonesRespuesta[i].onClick.RemoveAllListeners();
            botonesRespuesta[i].onClick.AddListener(() => SeleccionarRespuesta(index));
         //   pictogramas[i] = pregunta.pictograma[i];
        }
        Debug.Log(pregunta.audioNarracion);
        narradorAudioSource.clip = pregunta.audioNarracion;
        if (pregunta.audioNarracion != null)
        {
            narradorAudioSource.Stop();
            narradorAudioSource.clip = pregunta.audioNarracion;
            narradorAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Audio de narración no asignado.");
        }
    }

    /*private void SeleccionarRespuesta(int index)
    {
        bool correcta = preguntaManager.VerificarRespuesta(index);

        if (correcta)
        {
            Debug.Log("¡Correcto!");
            if (preguntaManager.AvanzarPregunta())
            {
                MostrarPregunta();
            }
            else
            {
                Debug.Log("Fin del cuestionario.");
                SceneManager.LoadScene("CompraComida");
            }
        }
        else
        {
            Debug.Log("Incorrecto. Inténtalo de nuevo.");
        }
    }*/
   /* private void SeleccionarRespuesta(int seleccion) // para cambio de color
    {
        TipoRespuesta resultado = preguntaManager.VerificarRespuesta(seleccion);

        Color feedbackColor = resultado switch
        {
            TipoRespuesta.Correcta => colorCorrecto,
            TipoRespuesta.Parcial => colorParcial,
            _ => colorIncorrecto
        };

        botonesRespuesta[seleccion].image.color = feedbackColor;

        //foreach (var b in botonesRespuesta)
          //  b.interactable = false;

        StartCoroutine(ContinuarTrasFeedback(resultado == TipoRespuesta.Correcta));
    }*/

    private void SeleccionarRespuesta(int seleccion) //para cambio de Sprite
    {
        TipoRespuesta resultado = preguntaManager.VerificarRespuesta(seleccion);
        //RegistroError
        if (resultado == TipoRespuesta.Incorrecta)
        {
            RegistrarError();
        }
        //Seleccion Sprite boton
        Sprite spriteFeedback = resultado switch
        {
            TipoRespuesta.Correcta => spriteCorrecto,
            TipoRespuesta.Parcial => spriteParcial,
            _ => spriteIncorrecto
        };

        botonesRespuesta[seleccion].image.sprite = spriteFeedback;
        //Seleccion sonido
        AudioClip sonidoFeedback = resultado switch
        {
            TipoRespuesta.Correcta => sonidoCorrecto,
            TipoRespuesta.Parcial => sonidoParcial,
            _ => sonidoIncorrecto
        };

        if (sonidoFeedback != null && audioUI != null)
            audioUI.PlayOneShot(sonidoFeedback);
        //desactivar botones por x segundos
        foreach (var b in botonesRespuesta)
            b.interactable = false;
        //Guardar respuesta en EvaluacionFinal
        Pregunta pregunta = preguntaManager.ObtenerPreguntaActual();
        EvaluacionFinal.Instancia.RegistrarRespuesta(
            pregunta.texto,
            pregunta.opciones[seleccion],
            pregunta.opciones[pregunta.respuestaCorrecta]
         );
        StartCoroutine(ContinuarTrasFeedback(resultado == TipoRespuesta.Correcta));
    }
    private IEnumerator ContinuarTrasFeedback(bool fueCorrecta)
    {
        yield return new WaitForSeconds(tiempoFeedback);

        if (fueCorrecta)
            preguntaManager.AvanzarPregunta();

        MostrarPregunta();
    }
    public void MostrarAyuda()
    {
        Pregunta pregunta = preguntaManager.ObtenerPreguntaActual();

        if (pregunta != null)
        {
            cuadroDeAyuda.SetActive(true);
            botonVolver.gameObject.SetActive(true);

            preguntaText.gameObject.SetActive(false);
            foreach (var boton in botonesRespuesta)
                boton.gameObject.SetActive(false);
            botonAyuda.gameObject.SetActive(false);
            StopAllCoroutines();
            StartCoroutine(TipearTexto(pregunta.textoAyuda));

            if (pregunta.audioAyuda != null && ayudaAudioSource != null)
            {
                ayudaAudioSource.Stop();
                ayudaAudioSource.clip = pregunta.audioAyuda;
                ayudaAudioSource.Play();
            }
        }
    }
    public void VolverDeAyuda()
    {
        cuadroDeAyuda.SetActive(false);
        botonVolver.gameObject.SetActive(false);

        preguntaText.gameObject.SetActive(true);
        foreach (var boton in botonesRespuesta)
            boton.gameObject.SetActive(true);
        botonAyuda.gameObject.SetActive (true);
        if (ayudaAudioSource != null && ayudaAudioSource.isPlaying)
        {
            ayudaAudioSource.Stop();
        }
    }
    private void RegistrarError()
    {
        if (errores < indicadoresErrores.Length)
        {
            indicadoresErrores[errores].sprite = spriteError;
        }

        errores++;

        if (errores >= erroresMaximos)
        {
            SceneManager.LoadScene(escenaDerrota);
        }
    }
    private IEnumerator TipearTexto(string textoCompleto)
    {
        textoAyuda.text = "";
        foreach (char letra in textoCompleto)
        {
            textoAyuda.text += letra;
            yield return new WaitForSeconds(velocidadTipeo);
        }
    }

}
