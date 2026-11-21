using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortadaControlador : MonoBehaviour
{
    [Header("Botones de la Portada")]
    [SerializeField] private Button botonJugar;
    [SerializeField] private Button botonControles;
    [SerializeField] private Button botonSalir;

    [Header("Configuración de Escena")]
    [SerializeField] private string nombreEscenaAlJugar = "SeleccionMundo";

    [Header("Panel de Controles")]
    [SerializeField] private GameObject panelControles;
    [SerializeField] private Button botonCerrarControles;

    private void Start()
    {
        Time.timeScale = 1f;

        if (panelControles != null)
            panelControles.SetActive(false);

        StartCoroutine(EnfocarSiguienteFrame(ObtenerBotonValido()));
    }

    private void Update()
    {
        if (EventSystem.current == null) return;

        // Si el panel de controles está abierto, permitir cerrarlo con B (buttonEast) o Escape
        if (panelControles != null && panelControles.activeInHierarchy)
        {
            bool cerrarPulsado =
                (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame) ||
                (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame);

            if (cerrarPulsado)
            {
                CerrarPanelControles();
                return;
            }

            if (EventSystem.current.currentSelectedGameObject == null &&
                botonCerrarControles != null &&
                botonCerrarControles.gameObject.activeInHierarchy &&
                botonCerrarControles.interactable)
            {
                EventSystem.current.SetSelectedGameObject(botonCerrarControles.gameObject);
            }

            return;
        }

        // En la portada principal: recuperar foco si quedó en null
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            Button objetivo = ObtenerBotonValido();
            if (objetivo != null)
            {
                EventSystem.current.SetSelectedGameObject(objetivo.gameObject);
            }
        }
    }

    // --- Métodos llamados por los botones (OnClick) ---

    public void OnClickJugar()
    {
        Time.timeScale = 1f;

        if (!string.IsNullOrWhiteSpace(nombreEscenaAlJugar))
        {
            SceneManager.LoadScene(nombreEscenaAlJugar);
        }
        else
        {
            Debug.LogWarning("PortadaControlador: 'nombreEscenaAlJugar' está vacío.");
        }
    }

    public void OnClickControles()
    {
        if (panelControles != null)
        {
            panelControles.SetActive(true);

            if (botonCerrarControles != null)
            {
                StartCoroutine(EnfocarSiguienteFrame(botonCerrarControles));
            }
        }
        else
        {
            Debug.Log("PortadaControlador: Botón Controles pulsado (asigna un panelControles o tu propia lógica).");
        }
    }

    public void CerrarPanelControles()
    {
        if (panelControles != null)
        {
            panelControles.SetActive(false);
        }

        if (botonControles != null)
        {
            StartCoroutine(EnfocarSiguienteFrame(botonControles));
        }
        else
        {
            StartCoroutine(EnfocarSiguienteFrame(ObtenerBotonValido()));
        }
    }

    public void OnClickSalir()
    {
        Debug.Log("PortadaControlador: Saliendo de la aplicación...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // --- Utilidades de foco ---

    private Button ObtenerBotonValido()
    {
        if (EsBotonValido(botonJugar)) return botonJugar;
        if (EsBotonValido(botonControles)) return botonControles;
        if (EsBotonValido(botonSalir)) return botonSalir;
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
