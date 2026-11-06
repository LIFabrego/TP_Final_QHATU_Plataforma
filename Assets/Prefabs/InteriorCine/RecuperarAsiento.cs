using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecuperarAsiento : MonoBehaviour
{
    private void Start()
    {
        string filaAsignada = PlayerPrefs.GetString("FilaAsignada", "");
        int numeroAsignado = PlayerPrefs.GetInt("NumeroAsignado", -1);

        if (!string.IsNullOrEmpty(filaAsignada) && numeroAsignado > 0)
        {
            Asiento[] asientos = FindObjectsOfType<Asiento>();
            foreach (Asiento asiento in asientos)
            {
                if (asiento.fila == filaAsignada && asiento.numero == numeroAsignado)
                {
                    asiento.asientoCorrecto = true;
                    asiento.ocupado = true; //para que los npc no puedan conseguir este asiento
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("No se encontró información del asiento asignado en PlayerPrefs.");
        }
    }
}
