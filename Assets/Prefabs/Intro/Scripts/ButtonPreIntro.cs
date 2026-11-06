using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPreIntro : MonoBehaviour
{
    public void CargaIntro()
    {
       SceneManager.LoadScene("DialogoIntro");
    }
}
