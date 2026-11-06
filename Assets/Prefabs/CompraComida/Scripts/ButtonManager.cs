using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void Atras()
    {
        SceneManager.LoadScene("Loby");
    }
    //public void Adelante()
    //{
    //    SceneManager.LoadScene("SalaCine");
    //}
}
