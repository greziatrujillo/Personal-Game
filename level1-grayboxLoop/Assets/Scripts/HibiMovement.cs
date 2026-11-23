using UnityEngine;

/// <summary>
/// level 3 character movement and mechanic 
/// </summary>
/// 
public class HibiMovement : MonoBehaviour
{
    // movement
    public float velocity = 15f;
    public Transform model;
    public float turnSpeed = 12f;

    // jump mechanics
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce = 13f;
    public Transform checkGround;
    public LayerMask groundLayer;
    public float groundRadius = 0.2f;

    // dash
    public KeyCode dashKey = KeyCode.LeftShift;
    public float dashSpeed = 25f;
    public float dashDuration = 0.25f;
    public float dashCooldown = 0.6f;
    public int maxAirDashes = 1;
    [SerializeField] private AudioClip dashClip;
    [SerializeField, Range(0f, 1f)] private float dashVolume = 0.7f;

    private bool dashInput;
    private bool isDashing;
    private float dashTimer;
    private float dashCooldownTimer;
    private float dashDir = 1f;
    private float facingZ = 1f;
    private int airDashesRemaining;

    private Rigidbody rb;
    private float inputZ;
    private bool onGround;
    private bool jumpInput;
    private Animator animator;
    private Vector3 movement;

    // audio
    [SerializeField] private AudioClip[] footstepClips;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private float stepInterval = 0.37f;
    [SerializeField, Range(0f, 1f)] private float footstepVolume = 0.4f;
    [SerializeField, Range(0f, 1f)] private float jumpVolume = 0.7f;
    [SerializeField, Range(0f, 0.3f)] private float pitchVariation = 0.1f;
    private float stepTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        animator = GetComponentInChildren<Animator>();

        if (model == null && animator != null)
            model = animator.transform;
    }

    void Update()
    {

        if (animator != null)
        {
            animator.SetFloat("velocity", Mathf.Abs(inputZ));
        }

        if (Time.timeScale == 0f) return;
        
        inputZ = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(jumpKey))
            jumpInput = true;

        if (Input.GetKeyDown(dashKey))
            dashInput = true;

        if (Mathf.Abs(inputZ) > 0.1f)
            facingZ = Mathf.Sign(inputZ);

        movement = new Vector3(0f, 0f, inputZ).normalized;

        if (model != null && movement.sqrMagnitude > 0.01f)
            model.rotation = Quaternion.Slerp(
                model.rotation, Quaternion.LookRotation(movement), turnSpeed * Time.deltaTime);

        //footsteps
        bool isWalking = Mathf.Abs(inputZ) > 0.1f && onGround && !isDashing;

        if (isWalking)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                SoundFXManager.instance.PlayRandomSoundFxClip(footstepClips, transform, footstepVolume, pitchVariation);
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void FixedUpdate()   
    {
        // check ground
        onGround = Physics.CheckSphere(checkGround.position, groundRadius, groundLayer, QueryTriggerInteraction.Ignore);

        Vector3 vel = rb.linearVelocity;

        // dash
        if (onGround && !isDashing)
            airDashesRemaining = maxAirDashes;

        dashCooldownTimer -= Time.fixedDeltaTime;

        // start a dash
        if (dashInput && !isDashing && dashCooldownTimer <= 0f && (onGround || airDashesRemaining > 0))
        {
            if (!onGround) airDashesRemaining--;

            isDashing = true;
            dashTimer = dashDuration;
            dashDir = facingZ;
            rb.useGravity = false; // fly straight across instead of dropping
            SoundFXManager.instance.PlaySoundFxClip(dashClip, transform, dashVolume, pitchVariation);
        }

        if (isDashing)
        {
            dashTimer -= Time.fixedDeltaTime;
            vel.z = dashDir * dashSpeed;
            vel.y = 0f;

            if (dashTimer <= 0f)
            {
                isDashing = false;
                rb.useGravity = true;
                dashCooldownTimer = dashCooldown;
                vel.z = inputZ * velocity;
            }
        }
        else
        {
            // horizontal movement
            vel.z = inputZ * velocity;

            // jump
            if (jumpInput && onGround)
            {
                vel.y = jumpForce;
                SoundFXManager.instance.PlaySoundFxClip(jumpClip, transform, jumpVolume, pitchVariation);
            }
        }

        dashInput = false;
        jumpInput = false;
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

