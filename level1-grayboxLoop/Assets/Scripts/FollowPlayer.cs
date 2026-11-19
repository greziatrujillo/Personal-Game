using UnityEngine;

//allow the camera to follow the player
public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float smoothTime = 0.15f;

    //camera limits
    public float minZ = 20f;
    public float maxZ = 340f;
    public float minY = 45f;
    public float maxY = 80f;

    private Vector3 offset;
    private Vector3 currentVelocity;

    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;

        targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}
