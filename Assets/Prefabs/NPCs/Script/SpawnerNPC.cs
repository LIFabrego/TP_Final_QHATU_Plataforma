using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerNPC : MonoBehaviour
{
    [Header("Prefab del NPC")]
    [SerializeField] private GameObject npcPrefab;

    [Header("Cantidad de NPCs")]
    [SerializeField] private int cantidadMin = 5;
    [SerializeField] private int cantidadMax = 10;

    [Header("Tiempo de Spawn")]
    [SerializeField] private float tiempoEntreNPCs = 1.0f;
    [SerializeField] private float delayInicio = 0.5f;

    private int cantidadNPCsObjetivo;
    private int cantidadNPCsCreados;

    private void Start()
    {
        cantidadNPCsObjetivo = Random.Range(cantidadMin, cantidadMax + 1);
        cantidadNPCsCreados = 0;

        InvokeRepeating(nameof(SpawnNPC), delayInicio, tiempoEntreNPCs);
    }

    private void SpawnNPC()
    {
        if (cantidadNPCsCreados >= cantidadNPCsObjetivo)
        {
            CancelInvoke(nameof(SpawnNPC)); // Detiene el bucle
            return;
        }

        Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);
        Instantiate(npcPrefab, transform.position + offset, Quaternion.identity);

        cantidadNPCsCreados++;
    }
}
