using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioSourceManager : MonoBehaviour
{
    public static AudioSourceManager Instance;
    private AudioSource audioSource;
    [Header("Mensajes")]
    [SerializeField] private string mensajePregunta = "¿Querés sentarte en el asiento ";
    [SerializeField] private string mensajeError = "Este no es tu asiento. Seguí buscando.";
    [SerializeField] private string mensajeCorrecto = "¡Correcto! Este es tu asiento.";
    [SerializeField] private string mensajePermisoNPC = "¿Deseás pedir permiso para pasar a tu asiento?";

    [Header("Audios Mensaje")]
    [SerializeField] private AudioClip audioPregunta;
    [SerializeField] private AudioClip audioCorrecto;
    [SerializeField] private AudioClip audioSeguirBuscando;
    [SerializeField] private AudioClip audioPermisoNPC;

    [Header("Audios por fila (A-F)")]
    [SerializeField] private AudioClip audioFilaA;
    [SerializeField] private AudioClip audioFilaB;
    [SerializeField] private AudioClip audioFilaC;
    [SerializeField] private AudioClip audioFilaD;
    [SerializeField] private AudioClip audioFilaE;

    [Header("Audios por número (1-7)")]
    [SerializeField] private AudioClip audioNumero1;
    [SerializeField] private AudioClip audioNumero2;
    [SerializeField] private AudioClip audioNumero3;
    [SerializeField] private AudioClip audioNumero4;
    [SerializeField] private AudioClip audioNumero5;
    [SerializeField] private AudioClip audioNumero6;
    [SerializeField] private AudioClip audioNumero7;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        audioSource = GetComponent<AudioSource>();
    }
    public void ManejarAsientoDetectado(string fila, int numero, bool correcto, Action onConfirmar)
    {
        List<AudioClip> audios = new List<AudioClip>();

        if (audioPregunta != null) audios.Add(audioPregunta);

        AudioClip audioFila = ObtenerAudioFila(fila);
        AudioClip audioNumero = ObtenerAudioNumero(numero);

        if (audioFila != null) audios.Add(audioFila);
        if (audioNumero != null) audios.Add(audioNumero);

        string mensaje = $"{mensajePregunta}{fila}{numero}?";

        /*DialogoConfirmacion.Instance.MostrarPregunta(mensaje, null, onConfirmar);
        DialogoConfirmacion.Instance.MostrarMensajeSolo(mensaje, audios);*/
        DialogoConfirmacion.Instance.MostrarPreguntaConAudios(mensaje, audios, onConfirmar);
    }
    public void ManejarPermisoNPC(Action onConfirmar)
    {
        if (audioPermisoNPC != null)
        {
            DialogoConfirmacion.Instance.MostrarPregunta(mensajePermisoNPC, audioPermisoNPC, onConfirmar);
        }
        else
        {
            DialogoConfirmacion.Instance.MostrarPregunta(mensajePermisoNPC, null, onConfirmar);
        }
    }
    public void ManejarIntentoSentarse(bool esCorrecto)
    {
        if (esCorrecto)
        {
            DialogoConfirmacion.Instance.MostrarMensajeSolo(mensajeCorrecto, audioCorrecto);
        }
        else
        {
            DialogoConfirmacion.Instance.MostrarMensajeSolo(mensajeError, audioSeguirBuscando);
        }
    }
    private AudioClip ObtenerAudioFila(string fila)
    {
        return fila.ToUpper() switch
        {
            "A" => audioFilaA,
            "B" => audioFilaB,
            "C" => audioFilaC,
            "D" => audioFilaD,
            "E" => audioFilaE,
            _ => null,
        };
    }

    private AudioClip ObtenerAudioNumero(int numero)
    {
        return numero switch
        {
            1 => audioNumero1,
            2 => audioNumero2,
            3 => audioNumero3,
            4 => audioNumero4,
            5 => audioNumero5,
            6 => audioNumero6,
            7 => audioNumero7,
            _ => null,
        };
    }
}
