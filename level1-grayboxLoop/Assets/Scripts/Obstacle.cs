using UnityEngine;

/// <summary>
/// This script allows obstacles to inflict damage on the player
/// </summary>
public class Obstacle : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoundManager manager = Object.FindFirstObjectByType<RoundManager>();
            if (manager != null) manager.Lose();
        }
    }
}
