using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerHistoria : MonoBehaviour
{
    public void Atras()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
