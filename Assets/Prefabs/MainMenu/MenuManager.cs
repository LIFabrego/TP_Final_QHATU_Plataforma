using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
   public void PlayGame()
   {
     SceneManager.LoadScene("Loby"); 
   }
   public void OpenHistory()
   {
     SceneManager.LoadScene("Historia");
   }
   public void QuitGame()
   {
//#if UNITY_EDITOR
    //UnityEditor.EditorApplication.isPlaying = false;
//#elif UNITY_ANDROID
   // En Android, no es necesario cerrar manualmente
//#elif UNITY_STANDALONE
   Application.Quit();
//#endif
   }
}
