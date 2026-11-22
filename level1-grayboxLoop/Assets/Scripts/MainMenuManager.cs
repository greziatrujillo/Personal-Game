using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manage any main menu logic
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioClip menuSelectClip;
    public void PressStartGame()
    {
        SoundFXManager.instance.PlaySoundFxClip(menuSelectClip, transform);
        Time.timeScale = 1f;
        Invoke(nameof(LoadGame), 0.3f);
    }

    private void LoadGame()
    {
        SceneLoader.Load("SampleScene");
    }
}