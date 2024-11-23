using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("KeyBinds")] 
    [SerializeField] private KeyCode JumpKey;
    [SerializeField] private KeyCode DashKey;
    [SerializeField] private KeyCode CrouchKey;

    public Rigidbody rb;
    
    [SerializeField] private float speed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float desiredSpeed;
    [SerializeField] private float xInput;
    [SerializeField] private float zInput;
    [SerializeField] private Vector3 input;
    [SerializeField] private Vector3 properMoveDirection;
    [SerializeField] public bool isGrounded;

    [Header("Dash stuff")]
    private Vector3 DashStartPos;
    private Vector3 DashEndPos;
    private float DashStartTime;
    public float DashDistance;
    public float DashTime;
    public float DashCooldown = .5f;

    [Header("Ground stuff")]
    public float rayDistance;
    public LayerMask grouundLayer;

    [Header("Jump stuff")] 
    public float jumpForce;
    public bool CanJump = true;

    [Header("Gravity stuff")] 
    public float gravityStrength;
    public ConstantForce cf;


    public state playerState;
    public enum state{
        normal,
        dashing
    }

    void Update()
    {
        GetInput();
        Grounded();

        // Apply artificial gravity if we're  not grounded
        if (!isGrounded)
        {
            cf.enabled = true;
            cf.force = new Vector3(0f, -gravityStrength, 0f);
        }
        else {cf.enabled = false;}

        switch (playerState)
        {
            case state.normal:
                properMoveDirection = ApplyProperMoveDirection(); // Based on camera view
                transform.position +=  properMoveDirection*desiredSpeed * Time.deltaTime;
                break;
            case state.dashing:
                // Dive towards the end position (MoveTowards doesn't work as well, gonna have to see why).
                transform.position = Vector3.Lerp(transform.position, DashEndPos, DashTime);

                // Once we reach the end position, recover
                if (Vector3.Distance(transform.position, DashEndPos) <= 1) { playerState = state.normal; }
                break;
        }
    }

    void GetInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
        input = new Vector3(xInput, 0f, zInput);

        if (playerState == state.normal)
        {
            if (Input.GetKey(CrouchKey)) { desiredSpeed = crouchSpeed; }
            else { desiredSpeed = speed; }

            if (Input.GetKeyDown(DashKey) && playerState != state.dashing && Time.time - DashStartTime > DashCooldown)
            {
                desiredSpeed = 0.0f;

                // Store starting parameters.
                DashStartPos = transform.position;
                DashEndPos =  DashStartPos + (properMoveDirection.normalized * DashDistance);
                DashStartTime = Time.time;
        
                playerState = state.dashing;
            }

            if (Input.GetKeyDown(JumpKey) && isGrounded && CanJump)
            {
                CanJump = false;
                
                // jump
                rb.velocity += Vector3.up * jumpForce;

                Invoke(nameof(ResetCanJump), .4f);
            }
        }
    }

    void ResetCanJump()
    {
        CanJump = true;
    }

    void Grounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance, grouundLayer);
    }

    Vector3 ApplyProperMoveDirection()
    {
        // Use playerOrientation's forward and right vectors
        Vector3 forward = transform.forward; // Forward relative to camera
        Vector3 right = transform.right;    // Right relative to camera

        // Normalize to ignore any unintended scaling
        forward.Normalize();
        right.Normalize();

        // Combine input with directional vectors
        return (forward * input.z + right * input.x).normalized;
    }
}