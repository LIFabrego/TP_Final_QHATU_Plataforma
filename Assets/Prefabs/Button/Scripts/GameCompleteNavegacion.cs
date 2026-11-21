using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameCompleteNavegacion : MonoBehaviour
{
    [SerializeField] private Button[] botonesEnOrden; // Arrastra los 3 botones de izquierda a derecha

    private void Start()
    {
        StartCoroutine(EnfocarSiguienteFrame(ObtenerPrimerBotonValido()));
    }

    private void Update()
    {
        if (EventSystem.current == null) return;

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            Button objetivo = ObtenerPrimerBotonValido();
            if (objetivo != null)
            {
                EventSystem.current.SetSelectedGameObject(objetivo.gameObject);
            }
        }
    }

    private Button ObtenerPrimerBotonValido()
    {
        if (botonesEnOrden == null) return null;

        foreach (var boton in botonesEnOrden)
        {
            if (boton != null && boton.gameObject.activeInHierarchy && boton.interactable)
                return boton;
        }
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
