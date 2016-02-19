using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerMovementNetworkked : NetworkBehaviour
{
    public float accelerationSpeed;
    public float maxHorizontalSpeed;
    public float horizontalDrag;
    public float maxVerticalSpeed;
    public float jumpSpeed;
    public float jumpInputLenght;
    public float gravity;
    public float groundDetectionRange;
    public LayerMask groundLayer = -1;
    public Raycastpoints raycastpoints;

    new private Rigidbody2D rigidbody;
    private enum JumpState { Grounded, Active, Falling }
    private JumpState jumpState = JumpState.Grounded;
    private bool canJump = false;
    private float jumpInputTimer = 0f;
    private float jumpAcceleration = 0.1f;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void OnStartLocalPlayer()
    {

    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        Vector2 movementVector = rigidbody.velocity;

        float inputHorizontal = Input.GetAxis("Horizontal");
        bool inputUp = Input.GetButton("Jump");
        bool inputDown = Input.GetButton("Crouch");

        float movementHorizontal = inputHorizontal * accelerationSpeed * Mathf.InverseLerp(0f, maxHorizontalSpeed, maxHorizontalSpeed - Mathf.Abs(movementVector.x));

        bool isGrounded = false;
        RaycastHit2D hit;

        for (int i = 0; i < raycastpoints.bottom.Length; i++)
        {
            if (hit = Physics2D.Raycast(raycastpoints.bottom[i].position, -transform.up, groundDetectionRange, groundLayer.value))
            {
                isGrounded = true;
            }
        }

        if (isGrounded && jumpState == JumpState.Falling)
        {
            jumpState = JumpState.Grounded;
            Debug.Log("Hit ground");
        }
        else if (!isGrounded && jumpState == JumpState.Grounded)
        {
            jumpState = JumpState.Falling;
            Debug.Log("Falling without jumping");
        }
        
        if (inputUp && jumpState == JumpState.Grounded)
        {
            movementVector.y = 0f;
            jumpState = JumpState.Active;
            Debug.Log("Jump started");
        }

        if (jumpState == JumpState.Active)
        {
            if (inputUp && jumpInputTimer < jumpInputLenght)
            {
                movementVector.y = Mathf.Lerp(jumpSpeed / 2, jumpSpeed, jumpInputTimer / jumpInputLenght);
                jumpInputTimer += Time.deltaTime;
                Debug.Log("Jump active " + jumpInputTimer.ToString("0.000"));
            }
            else
            {
                jumpInputTimer = 0f;
                jumpState = JumpState.Falling;
                Debug.Log("Jump ended");
            }
        }

        movementVector.x += movementHorizontal;

        movementVector.y -= gravity;

        if (inputHorizontal == 0f)
        {
            movementVector.x = Mathf.Lerp(movementVector.x, 0f, horizontalDrag);
        }

        movementVector = new Vector2(Mathf.Clamp(movementVector.x, -maxHorizontalSpeed, maxHorizontalSpeed), Mathf.Clamp(movementVector.y, -maxVerticalSpeed, maxVerticalSpeed));

        rigidbody.velocity = movementVector;
    }
}
