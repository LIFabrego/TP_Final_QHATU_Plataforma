using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerCine : MonoBehaviour
{
    public void Atras()
    {
        SceneManager.LoadScene("InteriorSala");
    }
    //public void Adelante()
    //{
    //    SceneManager.LoadScene("Final");
    //}
}
