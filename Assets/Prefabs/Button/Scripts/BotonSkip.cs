using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonSkip : MonoBehaviour
{
    [Header("Nombre de la escena a cargar")]
    [SerializeField]private string sceneToLoad;

    //[Header("Referencia al botón Skip")]
    //[SerializeField]private Button skipButton;

    /*void Start()
    {
        if (skipButton != null)
        {
            // Asignamos el evento al botón
            skipButton.onClick.AddListener(SkipScene);
        }
        else
        {
            Debug.LogWarning("No se asignó un botón de Skip en el inspector.");
        }
    }*/

    public void SkipScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("No se definió la escena a cargar.");
        }
    }

}
