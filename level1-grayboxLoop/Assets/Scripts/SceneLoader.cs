using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// pass scene names for the loading scene
/// </summary>
public static class SceneLoader
{
    public static string targetScene;

    public static void Load(string sceneName)
    {
        targetScene = sceneName;
        SceneManager.LoadScene("LoadingScene");  
    }
}