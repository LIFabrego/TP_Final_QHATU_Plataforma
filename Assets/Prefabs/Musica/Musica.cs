using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Musica : MonoBehaviour
{
    [SerializeField] private AudioSource musicaAmbiente;

    private static Musica instancia;

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject); 
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);

        if (!musicaAmbiente.isPlaying)
        {
            musicaAmbiente.Play();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene escena, LoadSceneMode modo)
    {
        if (escena.name == "Final") // Cambiá por el nombre real de tu escena final
        {
            musicaAmbiente.Stop();
            Destroy(gameObject); // Opcional: destruye el AudioManager
        }
    }
}
