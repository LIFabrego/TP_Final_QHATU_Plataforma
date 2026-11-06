using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjetivoUIController : MonoBehaviour
{
    public static ObjetivoUIController Instance;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI textoObjetivo;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persiste entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MostrarObjetivoCumplido(string descripcion)
    {
        textoObjetivo.text = $"✔ {descripcion}";
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        animator.SetTrigger("Mostrar");

    }

    public void OcultarCanvas()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
