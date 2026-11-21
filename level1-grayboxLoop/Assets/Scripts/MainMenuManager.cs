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
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene"); 
        SoundFXManager.instance.PlaySoundFxClip(menuSelectClip, transform);
    }
}
