using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogoConfirmacion : MonoBehaviour
{
    public static DialogoConfirmacion Instance;
    private Coroutine corutinaAudioActual;
    private bool cancelarReproduccion = false;
    private bool esPreguntaSiNo = false;

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
    private void Update()
    {
        if (panelDialogo == null || !panelDialogo.activeInHierarchy)
            return;

        // Mantener siempre un botón enfocado si se perdió la selección
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == null)
        {
            Button objetivoRespaldo = (esPreguntaSiNo && botonSi != null && botonSi.gameObject.activeInHierarchy)
                ? botonSi
                : botonNo;

            if (objetivoRespaldo != null && objetivoRespaldo.interactable)
            {
                EventSystem.current.SetSelectedGameObject(objetivoRespaldo.gameObject);
            }
        }

        if (Gamepad.current == null) return;

        // Botón B (buttonEast): niega en pregunta SI/NO o cierra mensaje OK
        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            if (botonNo != null && botonNo.gameObject.activeInHierarchy && botonNo.interactable)
            {
                botonNo.onClick.Invoke();
            }
            return;
        }

        // Botón A (buttonSouth):
        // - Si hay un botón seleccionado, el InputSystemUIInputModule disparará Submit -> OnClick() del botón enfocado.
        // - Si es pregunta SI/NO y por alguna razón no había botón enfocado, afirma con SI.
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == null)
            {
                if (esPreguntaSiNo && botonSi != null && botonSi.gameObject.activeInHierarchy && botonSi.interactable)
                {
                    botonSi.onClick.Invoke();
                }
                else if (!esPreguntaSiNo && botonNo != null && botonNo.gameObject.activeInHierarchy && botonNo.interactable)
                {
                    botonNo.onClick.Invoke();
                }
            }
        }
    }
    public bool EstaAbierto => panelDialogo != null && panelDialogo.activeInHierarchy;
    public void MostrarPregunta(string mensaje, AudioClip audioPregunta, Action onSi)
    {
        esPreguntaSiNo = true;
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
        StartCoroutine(EnfocarBotonSiguienteFrame(botonSi));
    }
    public void MostrarPreguntaConAudios(string mensaje, List<AudioClip> listaAudios, Action onConfirmar)
    {
        esPreguntaSiNo = true;
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
        StartCoroutine(EnfocarBotonSiguienteFrame(botonSi));
    }
    public void MostrarMensajeSolo(string mensaje, AudioClip audioMensaje)
    {
        esPreguntaSiNo = false;
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
        StartCoroutine(EnfocarBotonSiguienteFrame(botonNo));
    }
    public void MostrarMensajeSolo(string mensaje, List<AudioClip> listaAudios)
    {
        esPreguntaSiNo = false;
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
        StartCoroutine(EnfocarBotonSiguienteFrame(botonNo));
    }

    private IEnumerator EnfocarBotonSiguienteFrame(Button objetivo)
    {
        yield return null;

        if (EventSystem.current == null || objetivo == null)
            yield break;

        if (!objetivo.gameObject.activeInHierarchy || !objetivo.interactable)
            yield break;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(objetivo.gameObject);
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
