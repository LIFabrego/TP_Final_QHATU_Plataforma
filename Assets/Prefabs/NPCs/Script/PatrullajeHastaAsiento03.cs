using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrullajeHastaAsiento03 : MonoBehaviour
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
        // === SETUP ANIMACIONES ===
        var mundo = SeleccionMundoManager.Instance?.mundoSeleccionado ?? SeleccionMundoManager.Mundo.Humano;
        bool esHumano = (mundo == SeleccionMundoManager.Mundo.Humano);
        bool esNino = Random.value < 0.5f;

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
        else
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
        col.enabled = false;

        // === OBTENER DATOS DEL NPC ===
        var npcData = ManagerNPCs02.Instance.npcEstado.Find(x => x.id == id);

        // === ASIGNAR ASIENTO ===
        Asiento asientoElegido = null;

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

        // === BUSCAR FILA ===
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

        // === CONSTRUIR CAMINO SOLO UNA VEZ ===
        List<Transform> caminoFinal = new List<Transform>();
        caminoFinal.AddRange(fila.puntosCamino);
        caminoFinal.Add(asientoElegido.transform);
        puntosMovimiento = caminoFinal.ToArray();

        if (vaAsiento)
        {
            siguientePaso = 0; // Ida empieza en 0
        }
        else
        {
            siguientePaso = puntosMovimiento.Length - 1; // Regreso empieza en N
        }

        ActualizarAnimacion();
    }

    private void Update()
    {
        if (vaAsiento)
        {
            RecorrerIda();
        }
        else
        {
            RecorrerRegreso();
        }
    }

    // === IDA ===
    private void RecorrerIda()
    {
        if (puntosMovimiento == null || puntosMovimiento.Length == 0) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            puntosMovimiento[siguientePaso].position,
            velMovimiento * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, puntosMovimiento[siguientePaso].position) < distanciaMinima)
        {
            siguientePaso++;

            if (siguientePaso >= puntosMovimiento.Length)
            {
                AlLlegarAsiento();
                return;
            }

            ActualizarAnimacion();
        }
    }

    // === REGRESO ===
    private void RecorrerRegreso()
    {
        if (puntosMovimiento == null || puntosMovimiento.Length == 0) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            puntosMovimiento[siguientePaso].position,
            velMovimiento * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, puntosMovimiento[siguientePaso].position) < distanciaMinima)
        {
            siguientePaso--;

            if (siguientePaso < 0)
            {
                AlTerminarRegreso();
                return;
            }

            ActualizarAnimacion();
        }
    }

    private void AlLlegarAsiento()
    {
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", 0);
        col.enabled = true;

        var npcData = ManagerNPCs02.Instance.npcEstado.Find(x => x.id == id);
        npcData.vaAsiento = false;
        npcData.ultimaPosicion = transform.position;
    }

    private void AlTerminarRegreso()
    {
        ManagerNPCs02.Instance.npcEstado.RemoveAll(x => x.id == id);
        Destroy(gameObject);
    }

    private void ActualizarAnimacion()
    {
        Vector2 direccion = (puntosMovimiento[siguientePaso].position - transform.position).normalized;
        animator.SetFloat("X", direccion.x);
        animator.SetFloat("Y", direccion.y);
    }
}
