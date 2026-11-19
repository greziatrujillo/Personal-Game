using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script manages whether the level is complete or not
/// </summary>
public class RoundManager : MonoBehaviour
{
    public int keys = 3;
    public Renderer exit;

    private int collected = 0;
    private bool finished = false;
    public int hp = 3;

    public bool AllKeysCollected
    {
        get { return collected >= keys; }
    }

    public void CollectKeys()
    {
        if (finished) return;

        collected++;
        Debug.Log("Key Collected (" + collected + " of " + keys + ")");

        if (AllKeysCollected)
        {
            Debug.Log("All keys collected, time to reach the exit");
            if (exit != null) exit.material.color = Color.green;
        }
    }

    public void CompleteLevel()
    {
        if (finished) return;
        finished = true;
        Debug.Log("Level complete");
        
    }

    public void ReceiveDamage(int d)
    {
        if (finished) return;

    hp -= d;
    Debug.Log("Lives left: " + hp);

    if (hp <= 0)
    {
        Lose();
    }
    }

    public void FallOut()
{
    if (finished) return;
    finished = true;
    Debug.Log("Fell out of view, restarting");
    Invoke("Restart", 1.5f);
}

    public void Lose()
    {
        if (finished) return;
        finished = true;
        Debug.Log("You lost");
        if (exit != null) exit.material.color = Color.red;
        Invoke("Restart", 1.5f);
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
