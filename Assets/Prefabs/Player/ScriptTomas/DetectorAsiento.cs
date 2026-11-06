using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DetectorAsiento : MonoBehaviour
{
    private Asiento asientoCercano;
    private bool asientoYaEncontrado;
    private NPCPermiso npcCercano;
    /*public AudioSource fuenteAudio;
    [SerializeField] private AudioClip audioCorrecto;
    [SerializeField] private AudioClip audioSeguirBuscando;*/
    void Start()
    {
        asientoYaEncontrado = ObjetivosManager.Instance.Objetivos.Exists(o => o.descripcion == "Sala Cine" && o.cumplido);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (asientoYaEncontrado) return; //Si el objetivo ya fue cumplido impide la colision con asiento

        if (other.CompareTag("Asiento"))
        {
            asientoCercano = other.GetComponent<Asiento>();
            if (asientoCercano != null && (!asientoCercano.ocupado || asientoCercano.asientoCorrecto))
            {
                AudioSourceManager.Instance.ManejarAsientoDetectado(asientoCercano.fila, asientoCercano.numero, asientoCercano.asientoCorrecto, () =>
                {
                    AudioSourceManager.Instance.ManejarIntentoSentarse(asientoCercano.asientoCorrecto);
                    if (asientoCercano.asientoCorrecto)
                    {
                        StartCoroutine(CargarEscenaConRetraso(3f)); // O el tiempo que desees
                    }
                });
            }
                /*string nombreAsiento = asientoCercano.fila + asientoCercano.numero;

                //asientoCercano.ReproducirAudioPresentacion(fuenteAudio, this);

                DialogoConfirmacion.Instance.MostrarPregunta(
                    $"¿Querés sentarte en el asiento {nombreAsiento}?",
                    null,
                    IntentarSentarse
                );*/
        } else if (other.CompareTag("NPC"))
        {
            npcCercano = other.GetComponent<NPCPermiso>();
            if (npcCercano != null)
            {
                AudioSourceManager.Instance.ManejarPermisoNPC(() =>
                {
                    npcCercano.DarPermiso();
                });
            }

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Asiento"))
        {
            asientoCercano = null;
            DialogoConfirmacion.Instance.OcultarMensaje();
        }
        else if (other.CompareTag("NPC"))
        {
            npcCercano = null;
            DialogoConfirmacion.Instance.OcultarMensaje();
        }
    }

    void IntentarSentarse()
    {
        if (asientoCercano == null) return;
        AudioSourceManager.Instance.ManejarIntentoSentarse(asientoCercano.asientoCorrecto);
        
        /*if (asientoCercano.asientoCorrecto)
        {
            DialogoConfirmacion.Instance.MostrarMensajeSolo("¡Correcto! Este es tu asiento.", audioCorrecto);
            StartCoroutine(CargarEscenaConRetraso(2f));
        }
        else
        {
            DialogoConfirmacion.Instance.MostrarMensajeSolo("Este no es tu asiento. Seguí buscando.", audioSeguirBuscando);
        }*/
    }
    IEnumerator CargarEscenaConRetraso(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        SceneTransitionManager.Instance.SavePlayerPosition(transform.position, SceneManager.GetActiveScene().name);
        SceneTransitionManager.Instance.SetPreviousScene(SceneManager.GetActiveScene().name);
        UnityEngine.SceneManagement.SceneManager.LoadScene("SalaCine");
    }
}
