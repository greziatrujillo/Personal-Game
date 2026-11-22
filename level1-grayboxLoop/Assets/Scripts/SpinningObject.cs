using UnityEngine;

/// <summary>
/// Allow collectible object to spin
/// </summary>
public class SpinningObject : MonoBehaviour
{
    public float spinSpeed = 90f;

    void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.World);
    }
}
