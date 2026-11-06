using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameVisualManager : MonoBehaviour
{
    [Header("Fondos")]
    [SerializeField] private Sprite fondoGato;
    [SerializeField] private Sprite fondoHumano;
    [SerializeField] private SpriteRenderer fondo;
    [Header("NPCs")] 
    [SerializeField] private GameObject lentesGato;
    [SerializeField] private GameObject lentesHumano;
    void Start()
    {
        var mundo = SeleccionMundoManager.Instance?.mundoSeleccionado ?? SeleccionMundoManager.Mundo.Humano;


        // Cambiar fondo
        fondo.sprite = mundo == SeleccionMundoManager.Mundo.Gato ? fondoGato : fondoHumano;

        // Activar solo los NPCs correspondientes
        lentesGato.SetActive(mundo == SeleccionMundoManager.Mundo.Gato);
        lentesHumano.SetActive(mundo == SeleccionMundoManager.Mundo.Humano);
    }
}
