using UnityEngine;

/// <summary>
/// Enemy damage received when player hits enemy
/// </summary>

public class EnemyDamage : MonoBehaviour
{
    public int damage = 1;
    public RoundManager roundManager;

    void Start()
    {
        roundManager = FindFirstObjectByType<RoundManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            roundManager.ReceiveDamage(damage);
        }
    }
}
