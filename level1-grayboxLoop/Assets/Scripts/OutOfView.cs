using UnityEngine;

/// <summary>
/// When the player falls, they respawn and restart
/// </summary>
public class OutOfView : MonoBehaviour
{
    public float margin = 0.1f;    
    public float graceTime = 0.5f;

    private Camera cam;
    private RoundManager manager;
    private float timer;

    void Start()
    {
        cam = Camera.main;
        manager = Object.FindFirstObjectByType<RoundManager>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer < graceTime || cam == null || manager == null) return;

        Vector3 v = cam.WorldToViewportPoint(transform.position);

        bool outOfView = v.z < 0
            || v.x < -margin || v.x > 1 + margin
            || v.y < -margin || v.y > 1 + margin;

        if (outOfView)
        {
            manager.FallOut();
        }
    }
}
