using UnityEngine;

/// <summary>
/// allow enemies to move through patrol points
/// </summary>
public class EnemyMove : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed = 2f;
    public int patrolDest;
    public float turnSpeed = 10f;
    public Transform model;

    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (model == null && animator != null)
            model = animator.transform;
    }

    void Update()
    {
        if (patrolPoints == null) return;

        Vector3 target = patrolPoints[patrolDest].position;
        Vector3 previous = transform.position;

        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.2f)
            patrolDest = (patrolDest + 1) % patrolPoints.Length;

        Vector3 dir = transform.position - previous;
        dir.y = 0f;
        if (model != null && dir.sqrMagnitude > 0.00001f)
            model.rotation = Quaternion.Slerp(model.rotation,
                Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);

        // animation
        if (animator != null)
            animator.SetFloat("velocity", moveSpeed);
    }
}
