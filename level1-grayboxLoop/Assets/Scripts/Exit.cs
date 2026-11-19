using UnityEngine;

public class Exit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        RoundManager manager = Object.FindFirstObjectByType<RoundManager>();
        if (manager == null) return;

        if (manager.AllKeysCollected)
        {
            manager.CompleteLevel();
            other.gameObject.SetActive(false);
        } else
            Debug.Log("Must collect all keys to complete level");
    }
}
