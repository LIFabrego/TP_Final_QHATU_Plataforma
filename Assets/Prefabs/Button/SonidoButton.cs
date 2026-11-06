using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SonidoButton : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoClick;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GameObject.Find("AudioManager")?.GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        Button boton = GetComponent<Button>();
        boton.onClick.AddListener(ReproducirSonido);
    }

    void ReproducirSonido()
    {
        if (sonidoClick != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoClick);
        }
    }
}
