using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionMundoNPCs : MonoBehaviour
{
    [Header("AnimacionHumano")]
    [SerializeField] private GameObject animacionHumano;

    [Header("AnimacionGato")]
    [SerializeField] private GameObject animacionGato;
    private void Start()
    {
        var mundo = SeleccionMundoManager.Instance?.mundoSeleccionado ?? SeleccionMundoManager.Mundo.Humano;

        if (mundo == SeleccionMundoManager.Mundo.Gato)
        {
            animacionGato.SetActive(true);
            animacionHumano.SetActive(false);
        }
        else
        {
            animacionGato.SetActive(false);
            animacionHumano.SetActive(true);
        }
    }
}
