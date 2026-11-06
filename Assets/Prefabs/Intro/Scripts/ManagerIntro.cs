using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ManagerIntro : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        StartCoroutine(PlayVideoAfter(3f));
        videoPlayer.loopPointReached += OnVideoFinished;
    }
    public void SkipIntro()
    {
        CargarMenu();
    }
    void OnVideoFinished(VideoPlayer vp)
    {
        CargarMenu();
    }
    void CargarMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator PlayVideoAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        videoPlayer.Play();
    }

}