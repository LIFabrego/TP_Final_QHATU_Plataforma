using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrullarBlendTree : MonoBehaviour
{
    [Header("AnimacionHumano")]
    [SerializeField] private GameObject animacionHumano;

    [Header("AnimacionGato")]
    [SerializeField] private GameObject animacionGato;
    [SerializeField] private float velMovimiento;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;

    private int numeroAleatorio;
    private Animator animator;

    private void Start()
    {
        var mundo = SeleccionMundoManager.Instance?.mundoSeleccionado ?? SeleccionMundoManager.Mundo.Humano;

        if (mundo == SeleccionMundoManager.Mundo.Gato)
        {
            animacionGato.SetActive(true);
            animacionHumano.SetActive(false);
        }
        else
        {
            animacionGato.SetActive(false);
            animacionHumano.SetActive(true);
        }
        numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
        animator = GetComponentInChildren<Animator>();
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
        animator.SetFloat("X", direccion.x);
        animator.SetFloat ("Y", direccion.y);
    }
}
