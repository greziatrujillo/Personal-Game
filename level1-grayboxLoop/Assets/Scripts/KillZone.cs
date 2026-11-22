using UnityEngine;

/// <summary>
/// if player falls out of level, they restart
/// </summary>
public class KillZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoundManager manager = Object.FindFirstObjectByType<RoundManager>();
            if (manager != null) manager.FallOut();
        }
    }
}