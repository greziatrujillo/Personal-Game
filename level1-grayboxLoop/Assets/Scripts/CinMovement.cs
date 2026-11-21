using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CinMovement : MonoBehaviour
{
    // movement
    public float velocity = 15f;
    public Transform model;
    public float turnSpeed = 12f;

    // jump mechanics
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce = 13f;
    public float riseGravityMultiplier = 1.5f; 
    public Transform checkGround;
    public LayerMask groundLayer;
    public float groundRadius = 0.2f;

    // slow falling
    public float maxFallingVelocity = 6f; 
    public float gravityMultiplier = 0.6f; 

    private Rigidbody rb;
    private float inputZ;
    private bool onGround;
    private bool jumpInput;
    private Animator animator;
    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        animator = GetComponentInChildren<Animator>();

        if (model == null && animator != null)
            model = animator.transform;
    }

    void Update()
    {
        inputZ = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(jumpKey))
            jumpInput = true;

        movement = new Vector3(0f, 0f, inputZ).normalized;

        if (animator != null)
            animator.SetFloat("velocity", Mathf.Abs(inputZ));
            animator.SetBool("grounded", onGround);
            animator.SetFloat("yVelocity", rb.linearVelocity.y);

        if (model != null && movement.sqrMagnitude > 0.01f)
            model.rotation = Quaternion.Slerp(
                model.rotation, Quaternion.LookRotation(movement), turnSpeed * Time.deltaTime);
    }

    void FixedUpdate()   
    {
        // check ground
        onGround = Physics.CheckSphere(checkGround.position, groundRadius, groundLayer, QueryTriggerInteraction.Ignore);

        Vector3 vel = rb.linearVelocity;

        // custom gravity
        float gravityMult = vel.y > 0f ? riseGravityMultiplier : gravityMultiplier;
        vel.y += Physics.gravity.y * gravityMult * Time.fixedDeltaTime;

        //horizontal movement
        vel.z = inputZ * velocity;

        // jump mechanic 
        if (jumpInput && onGround)
            vel.y = jumpForce;

        jumpInput = false;

        // falling speed
        if (vel.y < -maxFallingVelocity)
            vel.y = -maxFallingVelocity;

        rb.linearVelocity = vel;


    }

    // shows ground check in scene view
    void OnDrawGizmosSelected()
    {
        if (checkGround == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(checkGround.position, groundRadius);
    }
}