using UnityEngine;

public class Exit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        RoundManager manager = Object.FindFirstObjectByType<RoundManager>();
        if (manager == null) return;

        manager.CompleteLevel();
        other.gameObject.SetActive(false);
    }
}
