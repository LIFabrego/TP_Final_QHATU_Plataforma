using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Lentes3D : MonoBehaviour
{
    [SerializeField] private AudioClip audioDialogo;
    [SerializeField] private Sprite fondoDialogo;
    [SerializeField] private string objetivoACompletar = "Recibió lentes 3D";

    private bool yaInteractuo = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || yaInteractuo)
            return;

        yaInteractuo = true;

        string[] filas = { "A", "B", "C", "D", "E", "F" };
        int filaIndex = Random.Range(0, filas.Length);
        int numeroAsiento = Random.Range(1, 7); // Asientos del 1 al 5

        string asientoAsignado = $"Fila {filas[filaIndex]}, Número {numeroAsiento}";

        // Guardar el asiento asignado en un GameManager (si tenés uno)
        PlayerPrefs.SetString("AsientoAsignado", asientoAsignado);

        string mensaje = $"¡Aquí tienes tus lentes 3D! Tu asiento es {asientoAsignado}.";

        // Mostrar el diálogo con fondo e incluir el audio
        //DialogoConfirmacion.Instance.MostrarDialogoConFondo(mensaje, fondoDialogo, audioDialogo);

        // Marcar como objetivo cumplido
        ObjetivosManager.Instance.MarcarCumplido(objetivoACompletar);
    }
}
