using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script manages whether the level is complete or not
/// </summary>
public class RoundManager : MonoBehaviour

{
    public int keys = 3;
    public int hp = 3;
    public UiManager UiManager;

    private int collected = 0;
    private bool finished = false;
    private int maxHp;

    [SerializeField] private AudioClip collectKeyClip;
    [SerializeField] private AudioClip receiveDamageClip;
    [SerializeField] private AudioClip fallOffClip;
    [SerializeField] private AudioClip completeLevelClip;
    [SerializeField] private AudioClip loseLevelClip;

    void Start()
    {
        maxHp = hp;
        UiManager.UpdateKeys(collected, keys);
        UiManager.UpdateHp(hp, maxHp);
    }

    public void CollectKeys()
    {
        if (finished) return;

        collected++;
        UiManager.UpdateKeys(collected, keys);

        SoundFXManager.instance.PlaySoundFxClip(collectKeyClip, transform);

    }

    public void CompleteLevel()
    {
        if (finished) return;
        finished = true;
        SoundFXManager.instance.PlaySoundFxClip(completeLevelClip, transform);
        
    }

    public void ReceiveDamage(int d)
    {
        if (finished) return;

        hp -= d;
        if (hp < 0) hp = 0;
        UiManager.UpdateHp(hp, maxHp);

        if (hp <= 0)
        {
            Lose();
        }

        SoundFXManager.instance.PlaySoundFxClip(receiveDamageClip, transform);
    }

    public void FallOut()
    {
        if (finished) return;
        finished = true;
        SoundFXManager.instance.PlaySoundFxClip(fallOffClip, transform);
        Invoke("Restart", 1f);
    }

    public void Lose()
    {
        if (finished) return;
        finished = true;
        SoundFXManager.instance.PlaySoundFxClip(loseLevelClip, transform, 0.5f);
        Invoke("Restart", 1.5f);
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
