using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogoConfirmacion : MonoBehaviour
{
    public static DialogoConfirmacion Instance;
    private Coroutine corutinaAudioActual;
    private bool cancelarReproduccion = false;
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private TextMeshProUGUI textoPregunta;
    [SerializeField] private Button botonSi;
    [SerializeField] private Button botonNo;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI textoBotonSi;
    [SerializeField] private TextMeshProUGUI textoBotonNo;
    private Action onConfirmar;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        panelDialogo.SetActive(false);
    }
    public void MostrarPregunta(string mensaje, AudioClip audioPregunta, Action onSi)
    {
        panelDialogo.SetActive(true);
        textoPregunta.text = mensaje;
        onConfirmar = onSi;

        // Mostrar ambos botones
        botonSi.gameObject.SetActive(true);
        botonNo.gameObject.SetActive(true);

        textoBotonSi.text = "SI";
        textoBotonNo.text = "NO";
        //Audio
        if (audioPregunta != null && audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = audioPregunta;
            audioSource.Play();
        }
        //Botones
        botonSi.onClick.RemoveAllListeners();
        botonSi.onClick.AddListener(() =>
        {
            panelDialogo.SetActive(false);
            onConfirmar?.Invoke();
        });

        botonNo.onClick.RemoveAllListeners();
        botonNo.onClick.AddListener(() =>
        {
            panelDialogo.SetActive(false);
        });
    }
    public void MostrarPreguntaConAudios(string mensaje, List<AudioClip> listaAudios, Action onConfirmar)
    {
        panelDialogo.SetActive(true);
        textoPregunta.text = mensaje;

        botonSi.gameObject.SetActive(true);
        botonNo.gameObject.SetActive(true);
        textoBotonSi.text = "SI";
        textoBotonNo.text = "NO";
        botonSi.onClick.RemoveAllListeners();
        botonNo.onClick.RemoveAllListeners();

        botonSi.onClick.AddListener(() =>
        {
            panelDialogo.SetActive(false);
            onConfirmar?.Invoke();
        });

        botonNo.onClick.AddListener(() =>
        {
            panelDialogo.SetActive(false);
        });
        if (corutinaAudioActual != null)
        {
            StopCoroutine(corutinaAudioActual);
        }

        if (listaAudios != null && listaAudios.Count > 0)
        {
            StartCoroutine(ReproducirAudiosSecuencial(listaAudios));
        }
    }
    public void MostrarMensajeSolo(string mensaje, AudioClip audioMensaje)
    {
        panelDialogo.SetActive(true);
        textoPregunta.text = mensaje;

        if (audioMensaje != null && audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = audioMensaje;
            audioSource.Play();
            StartCoroutine(CerrarPanelTrasAudio(audioMensaje.length));
        }
        botonSi.gameObject.SetActive(false);
        botonNo.gameObject.SetActive(true);
        textoBotonNo.text = "OK";
        botonNo.onClick.RemoveAllListeners();
        botonNo.onClick.AddListener(() =>
        {
            panelDialogo.SetActive(false);
            textoBotonNo.text = "NO";
        });
    }
    public void MostrarMensajeSolo(string mensaje, List<AudioClip> listaAudios)
    {
        panelDialogo.SetActive(true);
        textoPregunta.text = mensaje;
        if (audioSource != null)
        {
            audioSource.Stop(); // detiene audio anterior
        }
        botonSi.gameObject.SetActive(false);
        botonNo.gameObject.SetActive(true);
        textoBotonNo.text = "OK";
        botonNo.onClick.RemoveAllListeners();
        botonNo.onClick.AddListener(() =>
        {
            OcultarMensaje();
        });

        cancelarReproduccion = false;
        if (corutinaAudioActual != null)
        {
            StopCoroutine(corutinaAudioActual);
        }

        corutinaAudioActual = StartCoroutine(ReproducirAudiosYCerrar(listaAudios));
    }
    private IEnumerator ReproducirAudiosSecuencial(List<AudioClip> listaAudios)
    {
        foreach (AudioClip clip in listaAudios)
        {
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
                yield return new WaitForSeconds(clip.length + 0.1f); // + 0.0F modificar el valor para agregar pausa entre audio 
            }
        }
    }
    private IEnumerator CerrarPanelTrasAudio(float duracion)
    {
        yield return new WaitForSeconds(duracion + 0.5f); // espera medio segundo extra
        panelDialogo.SetActive(false);
    }
    private IEnumerator ReproducirAudiosYCerrar(List<AudioClip> audios)
    {
        foreach (AudioClip clip in audios)
        {
            if (cancelarReproduccion || clip == null) yield break;

            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.loop = false;
            audioSource.Play();

            while (audioSource.isPlaying)
            {
                if (cancelarReproduccion) yield break;
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);
        panelDialogo.SetActive(false);
    }
    public void OcultarMensaje()
    {
        cancelarReproduccion = true;

        if (corutinaAudioActual != null)
        {
            StopCoroutine(corutinaAudioActual);
            corutinaAudioActual = null;
        }

        audioSource.Stop();
        panelDialogo.SetActive(false);
    }

}
