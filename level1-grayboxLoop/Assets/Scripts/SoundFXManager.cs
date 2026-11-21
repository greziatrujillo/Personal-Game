using UnityEngine;
/// <summary>
/// Manage any sound effects to be used
/// </summary>

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;
    
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFxClip(AudioClip audioClip, Transform spawnTransform)
    {
        if (audioClip == null) return;
        
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}
