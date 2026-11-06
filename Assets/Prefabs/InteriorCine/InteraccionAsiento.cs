using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionAsiento : MonoBehaviour
{
    private bool jugadorEnZona = false;

    void Update()
    {
        if (jugadorEnZona && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("¡Encontraste tu asiento! Ahora podés ver la película.");
            // Aquí podrías activar una animación, sonido, o pasar a otra escena.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            jugadorEnZona = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            jugadorEnZona = false;
    }
}
