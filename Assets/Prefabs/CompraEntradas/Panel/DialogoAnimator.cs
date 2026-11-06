using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class DialogoAnimator : MonoBehaviour
    {
    public GameObject panelDialogo; // Panel que contiene el diálogo
    public float delay = 5f;        // Tiempo antes de mostrarlo

    private Vector3 escalaFinal = Vector3.one;

    void Start()
    {
        // Desactivamos el panel al inicio
        panelDialogo.transform.localScale = Vector3.zero;
        panelDialogo.SetActive(false);

        // Iniciamos la animación con delay
        Invoke("MostrarDialogo", delay);
    }

    void MostrarDialogo()
    {
        panelDialogo.SetActive(true);
        // Llamamos a una corrutina para animar la escala
        StartCoroutine(AparecerAnimacion());
    }

    System.Collections.IEnumerator AparecerAnimacion()
    {
        float duracion = 0.1f;
        float tiempo = 0f;
        Vector3 escalaInicial = Vector3.zero;

        while (tiempo < duracion)
        {
            float t = tiempo / duracion;
            panelDialogo.transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, t);
            tiempo += Time.deltaTime;
            yield return null;
        }

        panelDialogo.transform.localScale = escalaFinal;
    }
}
