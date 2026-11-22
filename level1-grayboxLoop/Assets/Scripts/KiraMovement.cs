using UnityEngine;

/// <summary>
/// player 2 movement script
/// </summary>
/// 
public class KiraPlayer : MonoBehaviour
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

    // double jump

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
        if (Time.timeScale == 0f) return;
        
        inputZ = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(jumpKey))
            jumpInput = true;

        movement = new Vector3(0f, 0f, inputZ).normalized;

        if (model != null && movement.sqrMagnitude > 0.01f)
            model.rotation = Quaternion.Slerp(
                model.rotation, Quaternion.LookRotation(movement), turnSpeed * Time.deltaTime);

        //footsteps
        bool isWalking = Mathf.Abs(inputZ) > 0.1f && onGround;

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

        //horizontal movement
        vel.z = inputZ * velocity;

        // jump mechanic 
        if (jumpInput && onGround)
        {
            vel.y = jumpForce;
            SoundFXManager.instance.PlaySoundFxClip(jumpClip, transform, jumpVolume, pitchVariation);
        }

        jumpInput = false;

    }

    // shows ground check in scene view
    void OnDrawGizmosSelected()
    {
        if (checkGround == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(checkGround.position, groundRadius);
    }
}

