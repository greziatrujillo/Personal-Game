using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manage any ui available to the player
/// </summary>
public class UiManager : MonoBehaviour
{
    public GameObject pauseUI;
    public void PressPause()
    {
        pauseUI.SetActive(true);
    }

    public void PressReturnToGame()
    {
        pauseUI.SetActive(false);
    }

    public void PressExitGame()
    {
        Application.Quit();
    }
}
