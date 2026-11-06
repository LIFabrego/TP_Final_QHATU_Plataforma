using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPermiso : MonoBehaviour
{
    private BoxCollider2D col;
    private bool permisoActivo = false;

    [SerializeField] private float tiempoPermiso = 3f; // Tiempo que se queda desactivado

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    public void DarPermiso()
    {
        if (!permisoActivo)
        {
            StartCoroutine(DesactivarTemporalmente());
        }
    }

    private IEnumerator DesactivarTemporalmente()
    {
        permisoActivo = true;
        col.enabled = false;
        yield return new WaitForSeconds(tiempoPermiso);
        col.enabled = true;
        permisoActivo = false;
    }
}
