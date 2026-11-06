using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    private Dictionary<string, Vector3> scenePositions = new();
    private string previousSceneName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerPosition(Vector3 position, string sceneName)
    {
        scenePositions[sceneName] = position;
    }

    public Vector3? GetSavedPosition(string sceneName)
    {
        if (scenePositions.TryGetValue(sceneName, out Vector3 pos))
        {
            return pos;
        }
        return null;
    }

    public void SetPreviousScene(string sceneName)
    {
        previousSceneName = sceneName;
    }

    public string GetPreviousScene()
    {
        return previousSceneName;
    }

    public void ClearAllPositions()
    {
        scenePositions.Clear();
    }
}
