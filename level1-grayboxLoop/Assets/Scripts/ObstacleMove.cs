using UnityEngine;

/// <summary>
/// Used for any moving obstacle
/// </summary>
public class ObstacleMove : MonoBehaviour
{
    public float velocity = 15f;
    public float reach = 25f; 
    public Vector3 direction = Vector3.up;
    private Vector3 origin;
    void Start()
    {
        origin = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Mathf.PingPong(Time.time * velocity, reach * 2f);
        transform.position = origin + direction.normalized * movement;
    }
}
