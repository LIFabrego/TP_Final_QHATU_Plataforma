using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionMundoManager : MonoBehaviour
{
    public enum Mundo { Gato, Humano }
    public static SeleccionMundoManager Instance;

    public Mundo mundoSeleccionado;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SeleccionarMundo(int tipo)
    {
        mundoSeleccionado = (Mundo)tipo;
        UnityEngine.SceneManagement.SceneManager.LoadScene("PreIntro");
    }
}
