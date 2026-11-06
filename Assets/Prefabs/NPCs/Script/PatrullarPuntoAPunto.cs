using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrullarPuntoAPunto : MonoBehaviour
{
    [SerializeField] private float velMovimiento;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;

    private int siguientePaso;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        ActualizarAnimacion();
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[siguientePaso].position, velMovimiento * Time.deltaTime);
        if (Vector2.Distance(transform.position, puntosMovimiento[siguientePaso].position) < distanciaMinima)
        {
            siguientePaso += 1;
            if (siguientePaso >= puntosMovimiento.Length)
            {
                siguientePaso = 0;
            }
            ActualizarAnimacion();
        }
    }
    private void ActualizarAnimacion()
    {
        Vector2 direccion = (puntosMovimiento[siguientePaso].position - transform.position).normalized;
        animator.SetFloat("X", direccion.x);
        animator.SetFloat("Y", direccion.y);
    }
}
