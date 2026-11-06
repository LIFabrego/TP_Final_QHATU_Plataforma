using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameVisualManager2 : MonoBehaviour
{
    [Header("Fondos")]
    [SerializeField] private Sprite fondoGato;
    [SerializeField] private Sprite fondoHumano;
    [SerializeField] private Image fondo;

    private void Start()
    {
        var mundo = SeleccionMundoManager.Instance?.mundoSeleccionado ?? SeleccionMundoManager.Mundo.Humano;

        // Cambiar fondo
        fondo.sprite = mundo == SeleccionMundoManager.Mundo.Gato ? fondoGato : fondoHumano;
    }
}
