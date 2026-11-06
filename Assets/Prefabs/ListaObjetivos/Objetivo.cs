using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Objetivo : MonoBehaviour
{
    public string descripcion;
    public bool cumplido;
    public Image imagenObjetivo; // imagen en la UI
    public Sprite spriteVacio;   // imagen sin check
    public Sprite spriteCheck;   // imagen con check
}
