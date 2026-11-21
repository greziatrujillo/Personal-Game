using UnityEngine;
using TMPro;

/// <summary>
/// Manage any ui available to the player
/// </summary>
public class UiManager : MonoBehaviour
{
    public GameObject pauseUI;
    public TextMeshProUGUI keysText;
    public TextMeshProUGUI hpText;

    public void PressPause()
    {
        pauseUI.SetActive(true);
    }

    public void PressReturnToGame()
    {
        pauseUI.SetActive(false);
    }

    public void PressExitToMenu()
    {
        
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
