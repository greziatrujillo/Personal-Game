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

    public void PlaySoundFxClip(AudioClip audioClip, Transform spawnTransform, float volume = 1f, float pitchVariation = 0f)
    {
        if (audioClip == null) return;

        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);

        audioSource.Play();

        Destroy(audioSource.gameObject, audioClip.length / audioSource.pitch);
    }

    public void PlayRandomSoundFxClip(AudioClip[] clips, Transform spawnTransform, float volume = 1f, float pitchVariation = 0f)
{
    if (clips == null || clips.Length == 0) return;

    int index = Random.Range(0, clips.Length);
    PlaySoundFxClip(clips[index], spawnTransform, volume, pitchVariation);
}
}
