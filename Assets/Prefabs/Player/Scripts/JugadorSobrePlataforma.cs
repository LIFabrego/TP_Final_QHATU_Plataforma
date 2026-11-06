using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorSobrePlataforma : MonoBehaviour
{
    private Transform jugadorTransform;
    private Vector3 ultimaPosicionPlataforma;

    private void Start()
    {
        ultimaPosicionPlataforma = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            jugadorTransform = collision.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            jugadorTransform = null;
        }
    }

    private void Update()
    {
        if (jugadorTransform != null)
        {
            Vector3 desplazamientoPlataforma = transform.position - ultimaPosicionPlataforma;
            jugadorTransform.position += desplazamientoPlataforma;
            ultimaPosicionPlataforma = transform.position;
        }
    }
}
