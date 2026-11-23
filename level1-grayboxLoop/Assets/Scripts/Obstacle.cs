using UnityEngine;

/// <summary>
/// This script allows obstacles to inflict damage on the player
/// </summary>
public class Obstacle : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoundManager manager = Object.FindFirstObjectByType<RoundManager>();
            if (manager != null) manager.ReceiveDamage(1);
        }
    }
}
