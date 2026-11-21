using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPausaControlador : MonoBehaviour
{

    [Header("Elementos de Pausa")]
    [SerializeField] private GameObject panelPausa; // Arrastra tu panel oscuro/UI de pausa
    [SerializeField] private GameObject panelControles; //arrastra tu panel de controles si lo tienes
    [SerializeField] private Button botonReanudar;
    [SerializeField] private Button botonControles;
    [SerializeField] private Button botonSalirMenu;
    [SerializeField] private Button botonCerrarControles; // Botón para cerrar el panel de controles

    [Header("Configuración")]
    [SerializeField] private string nombreEscenaMenuPrincipal = "Portada";
    [Tooltip("Tecla de teclado para pausar/despausar (alternativa a Gamepad Start)")]
    [SerializeField] private KeyCode teclaPausaKeyboard = KeyCode.P;

    private bool estaPausado = false;
    private bool controlesAbiertos = false;


    private void Start()
    {
        // Asegurar que el juego empieza sin pausar y el panel oculto
        Time.timeScale = 1f;
        estaPausado = false;
        controlesAbiertos = false;
        if (panelPausa != null)
            panelPausa.SetActive(false);
        if (panelControles != null)
            panelControles.SetActive(false);
    }

    private void Update()
    {
        if (EventSystem.current == null) return;

        // 1. Si el panel de controles está abierto: Escape (o B en mando) lo cierra y vuelve al menú de pausa
        if (estaPausado && controlesAbiertos)
        {
            bool cerrarControles =
                (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame) ||
                (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame);

            if (cerrarControles)
            {
                CerrarPanelControles();
                return;
            }

            if (EventSystem.current.currentSelectedGameObject == null && EsBotonValido(botonCerrarControles))
            {
                EventSystem.current.SetSelectedGameObject(botonCerrarControles.gameObject);
            }

            return;
        }
        // 2. Alternar pausa con Start (Menú) en mando o tecla P / Escape en teclado
        bool botonStartMando = Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame;
        bool teclaPausa = Keyboard.current != null &&
            (Keyboard.current.pKey.wasPressedThisFrame ||
             (teclaPausaKeyboard != KeyCode.None && Input.GetKeyDown(teclaPausaKeyboard)) ||
             Keyboard.current.escapeKey.wasPressedThisFrame);

        if (botonStartMando || teclaPausa)
        {
            if (estaPausado)
                ReanudarJuego();
            else
                PausarJuego();

            return;
        }

        // 3. Recuperar foco en el menú de pausa si quedó en null
        if (estaPausado && !controlesAbiertos && EventSystem.current.currentSelectedGameObject == null)
        {
            EnfocarPrimerBotonValido();
        }
    }

    public void PausarJuego()
    {
        estaPausado = true;
        controlesAbiertos = false;
        Time.timeScale = 0f;

        if (panelPausa != null)
            panelPausa.SetActive(true);

        if (panelControles != null)
            panelControles.SetActive(false);

        StartCoroutine(EnfocarSiguienteFrame(ObtenerPrimerBotonValido()));
    }

    public void ReanudarJuego()
    {
        estaPausado = false;
        controlesAbiertos = false;
        Time.timeScale = 1f;

        if (panelControles != null)
            panelControles.SetActive(false);

        if (panelPausa != null)
            panelPausa.SetActive(false);

        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnClickControles()
    {
        if (!estaPausado)
            estaPausado = true;

        Time.timeScale = 0f;
        controlesAbiertos = true;

        if (panelPausa != null)
            panelPausa.SetActive(true);

        if (panelControles != null)
        {
            panelControles.SetActive(true);

            if (EsBotonValido(botonCerrarControles))
            {
                StartCoroutine(EnfocarSiguienteFrame(botonCerrarControles));
            }
            else if (EventSystem.current != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        else
        {
            Debug.LogWarning("MenuPausaControlador: asigna 'panelControles' en el Inspector.");
            controlesAbiertos = false;
            EnfocarPrimerBotonValido();
        }
    }

    public void OnClickSalirAMenu()
    {
        Time.timeScale = 1f; // Importante: restaurar el tiempo antes de cambiar de escena
        if (!string.IsNullOrWhiteSpace(nombreEscenaMenuPrincipal))
        {
            SceneManager.LoadScene(nombreEscenaMenuPrincipal);
        }
        else
        {
            Debug.LogWarning("MenuPausa: 'nombreEscenaMenuPrincipal' no está configurado.");
        }
    }

    public void CerrarPanelControles()
    {
        controlesAbiertos = false;
        Time.timeScale = 0f;

        if (panelControles != null)
            panelControles.SetActive(false);

        if (panelPausa != null)
            panelPausa.SetActive(true);

        Button objetivo = EsBotonValido(botonControles) ? botonControles : ObtenerPrimerBotonValido();
        StartCoroutine(EnfocarSiguienteFrame(objetivo));
    }
    // --- Utilidades ---

    private void EnfocarPrimerBotonValido()
    {
        if (EventSystem.current == null) return;

        // Intenta enfocar Reanudar, luego Controles, luego Salir.
        if (EsBotonValido(botonReanudar))
            EventSystem.current.SetSelectedGameObject(botonReanudar.gameObject);
        else if (EsBotonValido(botonControles))
            EventSystem.current.SetSelectedGameObject(botonControles.gameObject);
        else if (EsBotonValido(botonSalirMenu))
            EventSystem.current.SetSelectedGameObject(botonSalirMenu.gameObject);
    }
    private Button ObtenerPrimerBotonValido()
    {
        if (EsBotonValido(botonReanudar)) return botonReanudar;
        if (EsBotonValido(botonControles)) return botonControles;
        if (EsBotonValido(botonSalirMenu)) return botonSalirMenu;
        return null;
    }
    private bool EsBotonValido(Button boton)
    {
        return boton != null && boton.gameObject.activeInHierarchy && boton.interactable;
    }
    private IEnumerator EnfocarSiguienteFrame(Button objetivo)
    {
        yield return null;

        if (EventSystem.current == null || !EsBotonValido(objetivo))
            yield break;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(objetivo.gameObject);
    }
}
