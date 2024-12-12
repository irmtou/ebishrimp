using UnityEngine;

/*
    This script provides jumping and movement in Unity 3D - Gatsby
*/

public class Player : MonoBehaviour
{
    // Camera Rotation
    public float mouseSensitivity = 2f;
    public float rotationSmoothness = 10f;
    private float verticalRotation = 0f;
    private Transform cameraTransform;
    
    // Ground Movement
    private Rigidbody rb;
    public float moveSpeed = 5f;
    public float acceleration = 15f;
    public float deceleration = 20f;
    private float moveHorizontal;
    private float moveForward;

    // Jumping
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f; // Multiplies gravity when falling down
    public float ascendMultiplier = 2f; // Multiplies gravity for ascending to peak of jump
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter = 0f;
    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter = 0f;

    // Ground Detection
    public LayerMask groundLayer;
    private bool isGrounded = false;
    private float raycastDistance;

    // Sprinting
    public float sprintMultiplier = 1.5f;   // Multiplier for sprinting speed
    public float maxSprintDuration = 3f;   // Maximum time sprint can last
    private float sprintTimer = 0f;        // Timer to track sprint duration
    private bool isSprinting = false;     // Whether the player is sprinting
    public float sprintCooldown = 2f;     // Time required before sprinting again
    private float cooldownTimer = 0f;     // Timer to track cooldown

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cameraTransform = Camera.main.transform;

        // Set the raycast to be slightly beneath the player's feet
        float playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        raycastDistance = (playerHeight / 2) + 0.2f;

        // Hides the mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Sprint Input
        if (Input.GetKeyDown(KeyCode.LeftShift) && cooldownTimer <= 0f)
        {
            isSprinting = true;
            sprintTimer = maxSprintDuration; // Reset sprint timer
        }
        if (isSprinting)
        {
            sprintTimer -= Time.deltaTime;

            // Stop sprinting when the timer runs out
            if (sprintTimer <= 0f)
            {
                isSprinting = false;
                cooldownTimer = sprintCooldown; // Start cooldown
            }
        }
        else if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // Jumping and camera rotation
        RotateCamera();

        if (Input.GetButtonDown("Jump")){
            jumpBufferCounter = jumpBufferTime;
        }
        if(isGrounded){
            coyoteTimeCounter = coyoteTime;
        }
        else{
            coyoteTime -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f){
            Jump();
            jumpBufferCounter = 0f;
        }

    }

    void FixedUpdate()
    {
        isGrounded = CheckIfGrounded();
        MovePlayer();
        ApplyJumpPhysics();
    }

    void MovePlayer()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveForward = Input.GetAxisRaw("Vertical");

        Vector3 movement = (transform.right * moveHorizontal + transform.forward * moveForward).normalized;
        
        // Apply sprint multiplier if sprinting
        float currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;
        if(isSprinting){
            Debug.Log("Player is sprinting");
        }
        // Calculate target velocity
        Vector3 targetVelocity = movement * currentSpeed;

        // Smoothly adjust velocity for responsive movement
        Vector3 velocityChange = targetVelocity - new Vector3(rb.velocity.x, 0, rb.velocity.z);
        velocityChange = Vector3.ClampMagnitude(velocityChange, acceleration * Time.fixedDeltaTime);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        // If we aren't moving and are on the ground, stop velocity so we don't slide
        if (isGrounded && movement.magnitude == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    void RotateCamera()
    {
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); // Initial burst for the jump
    }

    void ApplyJumpPhysics()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (ascendMultiplier - 1) * Time.fixedDeltaTime;
        }
    }
    bool CheckIfGrounded()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        return Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, groundLayer);
    }
}