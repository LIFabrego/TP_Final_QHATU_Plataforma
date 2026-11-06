using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZonaCambioEscena1 : MonoBehaviour
{
    [SerializeField] private string nombreEscenaDestino;
    [SerializeField] private string mensajePregunta;
    [SerializeField] private string objetivoRequerido; // Ej: "Comprar entradas"
    [SerializeField] private string mensajeError; // Ej: "Debes tener entrada para comprar comida"
    [SerializeField] private AudioClip audioPregunta;
    [SerializeField] private AudioClip audioError;
    [SerializeField] private GameObject player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.CompareTag("Player"))
        {
            DialogoConfirmacion.Instance.MostrarPregunta(mensajePregunta, audioPregunta, () =>
            {
                // Acción si responde "Sí"
                UnityEngine.SceneManagement.SceneManager.LoadScene(nombreEscenaDestino);
            });
        }*/
        if (!other.CompareTag("Player"))
            return;

        bool requisitoCumplido = string.IsNullOrEmpty(objetivoRequerido) || ObjetivosManager.Instance.Objetivos.Exists(o => o.descripcion == objetivoRequerido && o.cumplido);

        DialogoConfirmacion.Instance.MostrarPregunta(mensajePregunta, audioPregunta, () =>
        {
            bool requisitoCumplido = string.IsNullOrEmpty(objetivoRequerido) ||
                ObjetivosManager.Instance.Objetivos.Exists(o => o.descripcion == objetivoRequerido && o.cumplido);

            if (requisitoCumplido)
            {
                SceneTransitionManager.Instance.SavePlayerPosition(player.transform.position, SceneManager.GetActiveScene().name);
                SceneTransitionManager.Instance.SetPreviousScene(SceneManager.GetActiveScene().name);
                UnityEngine.SceneManagement.SceneManager.LoadScene(nombreEscenaDestino);
            }
            else
            {
                DialogoConfirmacion.Instance.MostrarMensajeSolo(mensajeError, audioError);
            }
        });
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogoConfirmacion.Instance.OcultarMensaje();
        }
    }
}
