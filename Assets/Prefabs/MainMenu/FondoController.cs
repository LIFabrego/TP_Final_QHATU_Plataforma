using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FondoController : MonoBehaviour
{
    [SerializeField] private Image fondo;

    [Header("Gato")]
    [SerializeField] private Sprite fondoGato;

    [Header("Humano")]
    [SerializeField] private Sprite fondoHumano;

    private void Start()
    {
        if (SeleccionMundoManager.Instance != null && SeleccionMundoManager.Instance.mundoSeleccionado == SeleccionMundoManager.Mundo.Gato)
        {
            fondo.sprite = fondoGato;
        }
    }
}
