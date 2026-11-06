using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionRestorer : MonoBehaviour
{
    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Vector3? savedPos = SceneTransitionManager.Instance.GetSavedPosition(currentScene);

        if (savedPos != null)
        {
            transform.position = savedPos.Value;
        }
    }
}
