using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float accelerationSpeed;
    public float maxMoveSpeed;
    public float horizontalDrag;
    public float jumpSpeed;

    new private Rigidbody2D rigidbody;
    private bool isGrounded = false;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void Update()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        bool inputUp = Input.GetButtonDown("Jump");
        bool inputDown = Input.GetButton("Crouch");

        float movementHorizontal = inputHorizontal * accelerationSpeed * Mathf.InverseLerp(0f, maxMoveSpeed, maxMoveSpeed - Mathf.Abs(rigidbody.velocity.x));

        

        float movementVertical = 0f;

        if (inputUp)
        {
            movementVertical = jumpSpeed;
        }

        rigidbody.velocity += new Vector2(movementHorizontal, movementVertical);

        movementVertical = 0f;

        if (inputHorizontal == 0f)
        {
            rigidbody.velocity = new Vector2(Mathf.Lerp(rigidbody.velocity.x, 0f, horizontalDrag), rigidbody.velocity.y);
        }
    }
}
