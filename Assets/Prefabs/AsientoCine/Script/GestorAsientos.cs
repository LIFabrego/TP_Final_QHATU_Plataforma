using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorAsientos : MonoBehaviour
{
    public static GestorAsientos Instancia; // Singleton simple

    [Header("Todas las filas")]
    public CaminoFila[] filas; // Cada fila tiene su camino y asientos

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Asiento ObtenerAsientoAleatorio()
    {
        // 1. Elegir fila aleatoria
        List<CaminoFila> filasDisponibles = new List<CaminoFila>();
        foreach (var fila in filas)
        {
            if (HayAsientosLibres(fila))
            {
                filasDisponibles.Add(fila);
            }
        }

        if (filasDisponibles.Count == 0)
        {
            Debug.LogWarning("No quedan asientos libres.");
            return null;
        }

        CaminoFila filaSeleccionada = filasDisponibles[Random.Range(0, filasDisponibles.Count)];

        // 2. Buscar asiento libre en esa fila
        List<Asiento> libres = new List<Asiento>();
        foreach (var asiento in filaSeleccionada.asientos)
        {
            if (!asiento.ocupado)
            {
                libres.Add(asiento);
            }
        }

        if (libres.Count == 0)
        {
            Debug.LogWarning("Fila " + filaSeleccionada.nombreFila + " sin asientos libres.");
            return null;
        }

        // 3. Elegir asiento y marcar como ocupado
        Asiento elegido = libres[Random.Range(0, libres.Count)];
        elegido.ocupado = true;

        return elegido;
    }
    /// <summary>
    /// Busca un asiento ya asignado usando su ID único.
    /// </summary>
    public Asiento ObtenerAsientoPorID(int id)
    {
        foreach (var fila in filas)
        {
            foreach (var asiento in fila.asientos)
            {
                if (asiento.ID == id)
                {
                    return asiento;
                }
            }
        }

        Debug.LogWarning("No se encontró asiento con ID: " + id);
        return null;
    }
    private bool HayAsientosLibres(CaminoFila fila)
    {
        foreach (var asiento in fila.asientos)
        {
            if (!asiento.ocupado) return true;
        }
        return false;
    }
}
