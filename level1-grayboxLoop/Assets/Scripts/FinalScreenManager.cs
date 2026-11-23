using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// final scene management
/// </summary>

public class FinalScreenManager : MonoBehaviour
{
    [SerializeField] private AudioClip celebrationClip;
    [SerializeField, Range(0f, 1f)] private float celebrationVolume = 0.8f;
    

    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SoundFXManager.instance.PlaySoundFxClip(celebrationClip, transform, celebrationVolume);
    }

    public void PressContinue()
    {
        SceneLoader.Load("MainMenu");
    }
}
