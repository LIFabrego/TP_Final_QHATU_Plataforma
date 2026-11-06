using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonReinicio : MonoBehaviour
{
    public void ReiniciarJuego()
    {
        Debug.Log("Botón de reinicio presionado");
        // 1. Borrar PlayerPrefs para eliminar datos persistentes
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        // 2. limpia las posiciones guardadas
        if (SceneTransitionManager.Instance != null)
            SceneTransitionManager.Instance.ClearAllPositions();
        // 3. Reiniciar los objetivos
        if (ObjetivosManager.Instance != null)
        {
            ObjetivosManager.Instance.ReiniciarObjetivos();
        }
        //4. Reinciar Seleccion de mundo
        if (SeleccionMundoManager.Instance != null)
        {
            Destroy(SeleccionMundoManager.Instance.gameObject);
        }
        //Limpiar Valores de Evaluacion
        EvaluacionFinal.Instancia.respuestasJugador.Clear();
        // 4. Reiniciar escena inicial
        SceneManager.LoadScene("SeleccionMundo");
    }
    public void MostrarEvaluacion()
    {
        SceneManager.LoadScene("EvaluacionFinal");
    }
}
