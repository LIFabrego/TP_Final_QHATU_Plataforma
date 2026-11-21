using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverNavegacion : MonoBehaviour
{
    [SerializeField] private Button botonInicial; // Ej. Nueva Partida
    [SerializeField] private Button botonSecundario; // Ej. Salir

    private void Start()
    {
        StartCoroutine(EnfocarSiguienteFrame(ObtenerBotonValido()));
    }

    private void Update()
    {
        if (EventSystem.current == null) return;

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            Button objetivo = ObtenerBotonValido();
            if (objetivo != null)
            {
                EventSystem.current.SetSelectedGameObject(objetivo.gameObject);
            }
        }
    }

    private Button ObtenerBotonValido()
    {
        if (botonInicial != null && botonInicial.gameObject.activeInHierarchy && botonInicial.interactable)
            return botonInicial;

        if (botonSecundario != null && botonSecundario.gameObject.activeInHierarchy && botonSecundario.interactable)
            return botonSecundario;

        return null;
    }

    private IEnumerator EnfocarSiguienteFrame(Button objetivo)
    {
        yield return null;

        if (EventSystem.current == null || objetivo == null)
            yield break;

        if (!objetivo.gameObject.activeInHierarchy || !objetivo.interactable)
            yield break;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(objetivo.gameObject);
    }
}
