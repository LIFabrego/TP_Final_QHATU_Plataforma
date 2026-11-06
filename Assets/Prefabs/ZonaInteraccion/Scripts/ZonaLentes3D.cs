using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaLentes3D : MonoBehaviour
{
    [Header ("Mensajes")]
    [SerializeField] private string mensajePregunta;
    [SerializeField] private string mensajeError;
    [SerializeField] private string mensajeCorrecto;
    [SerializeField] private string objetivoRequerido;

    [Header("Audios base")]
    [SerializeField] private AudioClip audioPregunta;
    [SerializeField] private AudioClip audioError;
    [SerializeField] private AudioClip audioTieneAsiento;

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

    private bool lentesEntregados = false;
    private string[] filas = { "A", "B", "C", "D", "E" };
    private int[] numeros = { 1, 2, 3, 4, 5, 6, 7 };
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || lentesEntregados)
            return;

        bool requisitoCumplido = string.IsNullOrEmpty(objetivoRequerido) ||
            ObjetivosManager.Instance.Objetivos.Exists(o => o.descripcion == objetivoRequerido && o.cumplido);

        DialogoConfirmacion.Instance.MostrarPregunta(mensajePregunta, audioPregunta, () =>
        {
            if (requisitoCumplido)
            {
                lentesEntregados = true;

                // Marcar objetivo como cumplido
                ObjetivosManager.Instance.MarcarCumplido("Lentes3D");

                // Elegir asiento aleatorio
                string fila = filas[Random.Range(0, filas.Length)];
                int numero = numeros[Random.Range(0, numeros.Length)];

                // Guardar asiento en PlayerPrefs
                PlayerPrefs.SetString("FilaAsignada", fila);
                PlayerPrefs.SetInt("NumeroAsignado", numero);
                PlayerPrefs.Save();

                // Mostrar mensaje
                string mensajeAsiento = $"{mensajeCorrecto}: FILA {fila}, NUMERO {numero}";
                List<AudioClip> audios = new List<AudioClip>
                {
                    audioTieneAsiento,
                    GetAudioPorFila(fila),
                    GetAudioPorNumero(numero)
                };
                DialogoConfirmacion.Instance.MostrarMensajeSolo(mensajeAsiento, audios);
            }
            else
            {
                DialogoConfirmacion.Instance.MostrarMensajeSolo(mensajeError, audioError);
            }
        });
    }
    private AudioClip GetAudioPorFila(string fila)
    {
        return fila switch
        {
            "A" => audioFilaA,
            "B" => audioFilaB,
            "C" => audioFilaC,
            "D" => audioFilaD,
            "E" => audioFilaE,
            _ => null
        };
    }
    private AudioClip GetAudioPorNumero(int numero)
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
            _ => null
        };
    }

}
