using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIFichasDisplay : MonoBehaviour
{
    public TextMeshProUGUI textoFichas;
    public GameObject mensajeSinFichas;

    void Update()
    {
        if (FichaManager.instance != null)
        {
            int fichas = FichaManager.instance.ObtenerFichas();
            textoFichas.text = "Fichas: " + fichas;

            // Mostrar el mensaje si no quedan fichas
            if (fichas <= 0 && mensajeSinFichas != null)
            {
                mensajeSinFichas.SetActive(true);
            }
        }
    }
}