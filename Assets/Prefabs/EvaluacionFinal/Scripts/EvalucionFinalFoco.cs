using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EvaluacionFinalFoco : MonoBehaviour
{
    [SerializeField] private Button botonUnico;

    private void Start()
    {
        StartCoroutine(EnfocarSiguienteFrame());
    }

    private void Update()
    {
        if (EventSystem.current == null || botonUnico == null) return;

        if (EventSystem.current.currentSelectedGameObject == null &&
            botonUnico.gameObject.activeInHierarchy &&
            botonUnico.interactable)
        {
            EventSystem.current.SetSelectedGameObject(botonUnico.gameObject);
        }
    }

    private IEnumerator EnfocarSiguienteFrame()
    {
        yield return null;

        if (EventSystem.current == null || botonUnico == null)
            yield break;

        if (!botonUnico.gameObject.activeInHierarchy || !botonUnico.interactable)
            yield break;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(botonUnico.gameObject);
    }
}