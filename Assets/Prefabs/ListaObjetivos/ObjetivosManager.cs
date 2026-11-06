using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjetivosManager : MonoBehaviour
{
    public static ObjetivosManager Instance;

    [SerializeField] private List<Objetivo> objetivos = new List<Objetivo>();

    [SerializeField] private GameObject zonaCompraEntradas;
    [SerializeField] private GameObject zonaCompraComida;
    [SerializeField] private GameObject zonaIngresoSala;

    [SerializeField] private GameObject panelObjetivos;
    [SerializeField] private GameObject botonObjetivos;

    [SerializeField] private GameObject salidaFinal;

    public List<Objetivo> Objetivos => objetivos;

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
            return;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        ReiniciarObjetivos();
        foreach (Objetivo objetivo in objetivos)
        {
            objetivo.cumplido = PlayerPrefs.GetInt(objetivo.descripcion, 0) == 1;
            if (objetivo.cumplido && objetivo.imagenObjetivo != null && objetivo.spriteCheck != null)
                objetivo.imagenObjetivo.sprite = objetivo.spriteCheck;
        }
        if (panelObjetivos != null)
            panelObjetivos.SetActive(false);

        if (botonObjetivos != null)
            botonObjetivos.SetActive(false);

    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Escena cargada: " + scene.name);
        if (scene.name == "Loby" || scene.name == "InteriorSala")
        {
            if (botonObjetivos != null)
                botonObjetivos.SetActive(true);
            BuscarZonas();
            ActualizarZonas();
        } else
        {
            Debug.Log("EscenaNoEncontrada");
            if (botonObjetivos != null)
                botonObjetivos.SetActive(false);
        }
        /*if( scene.name == "InteriorSala")
        {
           if (botonObjetivos != null)
                botonObjetivos.SetActive(true);
            BuscarZonas();
            ActualizarZonas();
        }*/
            


    }
    private void BuscarZonas()
    {
        zonaCompraEntradas = GameObject.Find("ZonaCompraEntradas");
        zonaCompraComida = GameObject.Find("ZonaCompraComida");
        zonaIngresoSala = GameObject.Find("ZonaIngresoSalaCine");
        salidaFinal = GameObject.Find("Salida");
    }
    private void ActualizarZonas()
    {
        //bool entradas = objetivos.Exists(o => o.descripcion == "Comprar entradas" && o.cumplido);
        //bool comida = objetivos.Exists(o => o.descripcion == "Comprar comida" && o.cumplido);

        //zonaCompraEntradas.SetActive(!entradas);
        //zonaCompraComida.SetActive(entradas && !comida);
        //zonaIngresoSala.SetActive(entradas && comida);

        bool entradas = objetivos.Exists(o => o.descripcion == "Comprar entradas" && o.cumplido);
        bool comida = objetivos.Exists(o => o.descripcion == "Comprar comida" && o.cumplido);
        bool lentes3D = objetivos.Exists(o => o.descripcion == "Lentes3D" && o.cumplido);
        bool ingreso = objetivos.Exists(o => o.descripcion == "Sala Cine" && o.cumplido);

        if (zonaCompraEntradas != null)
            zonaCompraEntradas.SetActive(!entradas);
        if (zonaCompraComida != null)
            zonaCompraComida.SetActive(!comida);
        if (zonaIngresoSala != null)
            zonaIngresoSala.SetActive(!ingreso);
        if (salidaFinal != null)
            salidaFinal.SetActive(entradas && comida && lentes3D && ingreso);

    }
    public void MarcarCumplido(string descripcion)
    {
        Objetivo objetivo = objetivos.Find(o => o.descripcion == descripcion);
        if (objetivo != null && !objetivo.cumplido)
        {
            objetivo.cumplido = true;

            if (objetivo.imagenObjetivo != null && objetivo.spriteCheck != null)
                objetivo.imagenObjetivo.sprite = objetivo.spriteCheck;

            PlayerPrefs.SetInt(descripcion, 1);
            PlayerPrefs.Save();
            if (SceneManager.GetActiveScene().name == "Loby" || SceneManager.GetActiveScene().name == "InteriorSala")
                ActualizarZonas();
        }
    }
    public void ReiniciarObjetivos()
    {
        foreach (Objetivo objetivo in objetivos)
        {
            objetivo.cumplido = false;
            PlayerPrefs.SetInt(objetivo.descripcion, 0);

            if (objetivo.imagenObjetivo != null && objetivo.spriteVacio != null)
                objetivo.imagenObjetivo.sprite = objetivo.spriteVacio;
        }

        PlayerPrefs.Save();
        //ActualizarZonas();
    }
    public void TogglePanelObjetivos()
    {
        if (panelObjetivos != null)
            panelObjetivos.SetActive(!panelObjetivos.activeSelf);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


}
