using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manage any main menu logic
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    public void PressStartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene"); 
    }
}
