using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Manage any ui available to the player
/// </summary>
public class UiManager : MonoBehaviour
{
    public GameObject pauseUI;
    public TextMeshProUGUI keysText;
    public TextMeshProUGUI hpText;
    [SerializeField] private AudioClip menuSelectClip;

    public void PressPause()
    {
        pauseUI.SetActive(true);
        SoundFXManager.instance.PlaySoundFxClip(menuSelectClip, transform);
    }

    public void PressReturnToGame()
    {
        pauseUI.SetActive(false);
        SoundFXManager.instance.PlaySoundFxClip(menuSelectClip, transform);
    }

    public void PressExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        SoundFXManager.instance.PlaySoundFxClip(menuSelectClip, transform);
    }

    public void UpdateKeys(int current, int max)
    {
        keysText.text = "Keys " + current + "/" + max;
    }

    public void UpdateHp(int current, int max)
    {
        hpText.text = "HP " + current + "/" + max;
    }


}
