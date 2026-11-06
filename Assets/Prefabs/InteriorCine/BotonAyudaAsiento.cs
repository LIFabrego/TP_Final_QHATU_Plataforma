using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BotonAyudaAsiento : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private GameObject panelTexto; // Objeto que tiene el texto
    [SerializeField] private TMP_Text textoAyuda;

    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;

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
    public void MostrarAsientoAsignado()
    {
        string filaAsignada = PlayerPrefs.GetString("FilaAsignada", "");
        int numeroAsignado = PlayerPrefs.GetInt("NumeroAsignado", -1);

        if (!string.IsNullOrEmpty(filaAsignada) && numeroAsignado > 0)
        {
            textoAyuda.text = $" FILA {filaAsignada}, NUMERO {numeroAsignado}";
            AudioClip audioFila = ObtenerAudioFila(filaAsignada);
            AudioClip audioNumero = ObtenerAudioNumero(numeroAsignado);

            if (audioFila != null && audioNumero != null)
            {
                StartCoroutine(ReproducirAudiosSecuencia(audioFila, audioNumero));
            }
        }
        else
        {
            textoAyuda.text = "No se encontró información del asiento asignado.";
        }

        panelTexto.SetActive(true);
    }
    public void MostrarAsientoAsignadoConTiempo(float segundos = 10f)
    {
        MostrarAsientoAsignado();
        CancelInvoke(nameof(OcultarTexto));
        Invoke(nameof(OcultarTexto), segundos);
    }
    private AudioClip ObtenerAudioFila(string fila)
    {
        switch (fila.ToUpper())
        {
            case "A": return audioFilaA;
            case "B": return audioFilaB;
            case "C": return audioFilaC;
            case "D": return audioFilaD;
            case "E": return audioFilaE;
            default: return null;
        }
    }

    private AudioClip ObtenerAudioNumero(int numero)
    {
        switch (numero)
        {
            case 1: return audioNumero1;
            case 2: return audioNumero2;
            case 3: return audioNumero3;
            case 4: return audioNumero4;
            case 5: return audioNumero5;
            case 6: return audioNumero6;
            case 7: return audioNumero7;
            default: return null;
        }
    }
    private System.Collections.IEnumerator ReproducirAudiosSecuencia(AudioClip clip1, AudioClip clip2)
    {
        audioSource.Stop();
        audioSource.clip = clip1;
        audioSource.Play();
        yield return new WaitForSeconds(clip1.length + 0.2f);

        audioSource.clip = clip2;
        audioSource.Play();
    }
    public void OcultarTexto()
    {
        panelTexto.SetActive(false);
    }
}
