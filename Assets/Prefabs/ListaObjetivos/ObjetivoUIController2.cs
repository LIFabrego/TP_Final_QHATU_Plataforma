using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjetivoUIController2 : MonoBehaviour
{
    [SerializeField] private Transform contenedor;
    [SerializeField] private GameObject prefabObjetivoUI;

    void Start()
    {
        foreach (Objetivo objetivo in ObjetivosManager.Instance.Objetivos)
        {
            GameObject item = Instantiate(prefabObjetivoUI, contenedor);
            TextMeshProUGUI texto = item.GetComponentInChildren<TextMeshProUGUI>();
            texto.text = objetivo.descripcion;

            Toggle toggle = item.GetComponentInChildren<Toggle>();
            toggle.isOn = objetivo.cumplido;
            toggle.interactable = false;
        }
    }

    void OnEnable()
    {
        // refrescar si se reabre el panel
    }
}
