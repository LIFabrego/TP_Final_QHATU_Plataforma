using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerNPCs02 : MonoBehaviour
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
        if (ManagerNPCs02.Instance == null)
        {
            Debug.LogError("No existe ManagerNPCs02 en la escena.");
            return;
        }
        if (ManagerNPCs02.Instance.npcEstado.Count == 0)
        {
            cantidadNPCsObjetivo = Random.Range(cantidadMin, cantidadMax + 1);
            for (int i = 0; i < cantidadNPCsObjetivo; i++)
            {
                EstadoNPC nuevo = new EstadoNPC();
                nuevo.id = i;
                nuevo.vaAsiento = true; // Primera vez siempre va al asiento
                ManagerNPCs02.Instance.npcEstado.Add(nuevo);
            }
        }
        cantidadNPCsObjetivo = ManagerNPCs02.Instance.npcEstado.Count;
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
        // Tomo la data de este NPC
        var estadoNPC = ManagerNPCs02.Instance.npcEstado[cantidadNPCsCreados];
        Vector3 spawnPos;
        if ( estadoNPC.vaAsiento)
        {
           Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);
            spawnPos = transform.position + offset;
        }
        else
        {
            spawnPos = estadoNPC.ultimaPosicion;
        }
        
        GameObject npc = Instantiate(npcPrefab,spawnPos, Quaternion.identity);
        //Instantiate(npcPrefab, transform.position + offset, Quaternion.identity);
        // Le paso la info
        var script = npc.GetComponent<PatrullarHastaAsiento02>();
        script.id = estadoNPC.id;
        script.vaAsiento = estadoNPC.vaAsiento;


        cantidadNPCsCreados++;
    }
}
