using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrullarHastaAsiento02 : MonoBehaviour
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

    public int id;
    public bool vaAsiento;
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

        // Obtener estado del NPC
        var npcData = ManagerNPCs02.Instance.npcEstado.Find(x => x.id == id);
        // Elegir asiento correcto
        Asiento asientoElegido = null;
        // 1. Pedir asiento al gestor
        if (vaAsiento)
        {
            asientoElegido = GestorAsientos.Instancia.ObtenerAsientoAleatorio();
            npcData.asientoID = asientoElegido.ID;
        }
        else
        {
            asientoElegido = GestorAsientos.Instancia.ObtenerAsientoPorID(npcData.asientoID);
        }       
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
        if (vaAsiento)
        {
            siguientePaso = 0;
        } else
        {
            siguientePaso = puntosMovimiento.Length - 1;
        }

        ActualizarAnimacion();
    }
    private void Update()
    {
        if (vaAsiento)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                puntosMovimiento[siguientePaso].position,
                velMovimiento * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, puntosMovimiento[siguientePaso].position) < distanciaMinima)
            {
                if (vaAsiento) siguientePaso++;
                else siguientePaso--;

                bool fin = vaAsiento ? (siguientePaso >= puntosMovimiento.Length) : (siguientePaso < 0);
                if (fin)
                {
                    var npcData = ManagerNPCs02.Instance.npcEstado.Find(x => x.id == id);

                    if (vaAsiento)
                    {
                        animator.SetFloat("X", 0);
                        animator.SetFloat("Y", 0);
                        col.enabled = true;

                        npcData.vaAsiento = false;
                        npcData.ultimaPosicion = transform.position;
                    }
                    else
                    {
                        ManagerNPCs02.Instance.npcEstado.RemoveAll(x => x.id == id);
                        Destroy(gameObject);
                    }

                    return;
                }

                ActualizarAnimacion();
            }
        }
    }
    private void ActualizarAnimacion()
    {
        Vector2 direccion = (puntosMovimiento[siguientePaso].position - transform.position).normalized;
        animator.SetFloat("X", direccion.x);
        animator.SetFloat("Y", direccion.y);
    }
}
