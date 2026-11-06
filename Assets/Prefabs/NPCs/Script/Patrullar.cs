using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private float velMovimiento;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;

    private int numeroAleatorio;
    private Animator animator;

    private void Start()
    {
        numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
        animator = GetComponent<Animator>();
        ActualizarAnimacion();
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[numeroAleatorio].position, velMovimiento * Time.deltaTime);
        if (Vector2.Distance(transform.position, puntosMovimiento[numeroAleatorio].position) < distanciaMinima)
        {
            numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
            ActualizarAnimacion();
        }
    }
    private void ActualizarAnimacion()
    {
        Vector2 direccion = (puntosMovimiento[numeroAleatorio].position - transform.position).normalized;

        if (Mathf.Abs(direccion.x) > Mathf.Abs(direccion.y))
        {
            if (direccion.x > 0)
            {
                animator.Play("GuardiaRigth");
            } else
            {
                animator.Play("GuardiaLeft");
            } 
        }
        else
        {
            if (direccion.y > 0)
            {
                animator.Play("GuardiaUP");
            }
            else
            {
                animator.Play("GuardiaDown");
            }
        }
    }
}
