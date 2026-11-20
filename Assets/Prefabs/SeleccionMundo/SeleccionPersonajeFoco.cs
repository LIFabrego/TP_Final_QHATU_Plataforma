using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SeleccionPersonajeFoco : MonoBehaviour
{
    [SerializeField] private Button botonInicial;

    private void Start()
    {
        StartCoroutine(EnfocarSiguienteFrame(botonInicial));
    }

    private void Update()
    {
        if (EventSystem.current == null || botonInicial == null) return;

        if (EventSystem.current.currentSelectedGameObject == null &&
            botonInicial.gameObject.activeInHierarchy &&
            botonInicial.interactable)
        {
            EventSystem.current.SetSelectedGameObject(botonInicial.gameObject);
        }
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
