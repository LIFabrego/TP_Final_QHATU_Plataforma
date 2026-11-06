using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonEvaluacionVolver : MonoBehaviour
{
    public void Atras()
    {
        SceneManager.LoadScene("Final");
    }
}
