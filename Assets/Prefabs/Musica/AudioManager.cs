using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [System.Serializable]
    public class MusicaPorEscena
    {
        public string nombreEscena;
        public AudioClip clip;
    }

    [SerializeField] private List<MusicaPorEscena> musicaPorEscena;

    [SerializeField] private List<string> escenasSinMusica;
    [SerializeField] private float duracionFade = 0.5f;
    private static AudioManager instancia;

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene escena, LoadSceneMode modo)
    {
        string nombreActual = escena.name;

        if (escenasSinMusica.Contains(nombreActual))
        {
            StartCoroutine(FadeOutAndStop());
            return;
        }

        AudioClip nuevaMusica = ObtenerMusicaParaEscena(nombreActual);
        if (nuevaMusica != null && audioSource.clip != nuevaMusica)
        {
            StartCoroutine(CambiarMusicaSuavemente(nuevaMusica));
        }
    }

    private AudioClip ObtenerMusicaParaEscena(string escena)
    {
        foreach (var item in musicaPorEscena)
        {
            if (item.nombreEscena == escena)
            {
                return item.clip;
            }
        }
        return null;
    }
    private IEnumerator CambiarMusicaSuavemente(AudioClip nuevoClip)
    {
        yield return StartCoroutine(FadeOutAndStop());

        audioSource.clip = nuevoClip;
        audioSource.Play();

        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOutAndStop()
    {
        float inicioVolumen = audioSource.volume;

        for (float t = 0; t < duracionFade; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(inicioVolumen, 0, t / duracionFade);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();
    }

    private IEnumerator FadeIn()
    {
        string nombreEscena = SceneManager.GetActiveScene().name;
        float volumenFinal = 0.3f;
        if (nombreEscena == "CompraEntradas" || nombreEscena == "CompraComida" || nombreEscena == "SalaCine" || nombreEscena == "DialogoIntro")
        {
            volumenFinal = 0.04f;
        }
        for (float t = 0; t < duracionFade; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, volumenFinal, t / duracionFade);
            yield return null;
        }

        audioSource.volume = volumenFinal;
    }
}
