using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manage loading screen
/// </summary>
public class LoadingManager : MonoBehaviour
{

    [SerializeField] private Slider progressBar;
    [SerializeField] private float minLoadTime = 2f;
    [SerializeField] private string fallbackScene = "SampleScene";
    void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        string sceneToLoad = string.IsNullOrEmpty(SceneLoader.targetScene)
            ? fallbackScene
            : SceneLoader.targetScene;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        float timer = 0f;

        while (operation.progress < 0.9f || timer < minLoadTime)
        {
            timer += Time.deltaTime;
            float realProgress = Mathf.Clamp01(operation.progress / 0.9f);
            float timeProgress = Mathf.Clamp01(timer / minLoadTime);
            progressBar.value = Mathf.Min(realProgress, timeProgress);
            yield return null;
        }

        progressBar.value = 1f;
        operation.allowSceneActivation = true;
    }
}
