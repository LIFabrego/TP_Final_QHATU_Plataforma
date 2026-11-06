using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerButton : MonoBehaviour
{
    public void Atras()
    {
        SceneManager.LoadScene("Loby");
    }
    /*public void Adelante()
    {
        SceneManager.LoadScene("CompraComida");
    }*/
}
