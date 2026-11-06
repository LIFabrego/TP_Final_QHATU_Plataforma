using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EstadoNPC
{
    public int id;
    public bool vaAsiento;
    public int asientoID; // Opcional, si quieres guardar asiento asignado
    public Vector3 ultimaPosicion;
}
public class ManagerNPCs02 : MonoBehaviour
{
    public static ManagerNPCs02 Instance;
    public List<EstadoNPC> npcEstado = new List<EstadoNPC>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
