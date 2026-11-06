using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrullarHastaAsiento : MonoBehaviour
{
    [Header("Animaciones Humano")]
    [SerializeField] private GameObject animacionHumanoNino;
    [SerializeField] private GameObject animacionHumanoNina;

    [Header("Animaciones Gato")]
    [SerializeField] private GameObject animacionGatoNino;
    [SerializeField] private GameObject animacionGatoNina;

    [SerializeField] private float velMovimiento;
    [SerializeField] private float distanciaMinima = 0.1f;
    private Transform[] puntosMovimiento;
    private int siguientePaso;
    private Animator animator;

    private BoxCollider2D col;
    private void Start()
    {
        var mundo = SeleccionMundoManager.Instance?.mundoSeleccionado ?? SeleccionMundoManager.Mundo.Humano;
        bool esHumano = (mundo == SeleccionMundoManager.Mundo.Humano);
        bool esNino = Random.value < 0.5f; // 50% probabilidad
                                           // Desactivar todos
        animacionHumanoNino.SetActive(false);
        animacionHumanoNina.SetActive(false);
        animacionGatoNino.SetActive(false);
        animacionGatoNina.SetActive(false);

        if (esHumano)
        {
            if (esNino)
            {
                animacionHumanoNino.SetActive(true);
                animator = animacionHumanoNino.GetComponent<Animator>();
            }
            else
            {
                animacionHumanoNina.SetActive(true);
                animator = animacionHumanoNina.GetComponent<Animator>();
            }
        }
        else // Es Gato
        {
            if (esNino)
            {
                animacionGatoNino.SetActive(true);
                animator = animacionGatoNino.GetComponent<Animator>();
            }
            else
            {
                animacionGatoNina.SetActive(true);
                animator = animacionGatoNina.GetComponent<Animator>();
            }
        }
        animator = GetComponentInChildren<Animator>();
        col = GetComponent<BoxCollider2D>();
        col.enabled = false; // Modo fantasma mientras camina
        // 1. Pedir asiento al gestor
        Asiento asientoElegido = GestorAsientos.Instancia.ObtenerAsientoAleatorio();
        if (asientoElegido == null)
        {
            Debug.LogError("No se pudo asignar asiento.");
            return;
        }

        //asientoElegido.asientoCorrecto = true;

        // 2. Buscar la fila a la que pertenece el asiento
        CaminoFila fila = null;
        foreach (var f in GestorAsientos.Instancia.filas)
        {
            foreach (var a in f.asientos)
            {
                if (a == asientoElegido)
                {
                    fila = f;
                    break;
                }
            }
            if (fila != null) break;
        }

        if (fila == null)
        {
            Debug.LogError("No se encontró la fila del asiento.");
            return;
        }

        // 3. Construir camino final: camino base + asiento
        List<Transform> caminoFinal = new List<Transform>();
        caminoFinal.AddRange(fila.puntosCamino);
        caminoFinal.Add(asientoElegido.transform);

        puntosMovimiento = caminoFinal.ToArray();

        ActualizarAnimacion();
    }
    private void Update()
    {
        if (puntosMovimiento == null || puntosMovimiento.Length == 0) return;

        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[siguientePaso].position, velMovimiento * Time.deltaTime);
        if (Vector2.Distance(transform.position, puntosMovimiento[siguientePaso].position) < distanciaMinima)
        {
            siguientePaso += 1;
            if (siguientePaso >= puntosMovimiento.Length)
            {
                animator.SetFloat("X", 0);
                animator.SetFloat("Y", 0);
                col.enabled = true;
                return; //npc llego a su asiento
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
