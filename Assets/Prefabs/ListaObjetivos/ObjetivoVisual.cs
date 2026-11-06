using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetivoVisual : MonoBehaviour
{
    [SerializeField] private Image objetivoImage;
    [SerializeField] private Sprite spriteVacio;
    [SerializeField] private Sprite spriteCheck;

    public void MarcarCumplido()
    {
        objetivoImage.sprite = spriteCheck;
    }

    public void Reiniciar()
    {
        objetivoImage.sprite = spriteVacio;
    }
}
