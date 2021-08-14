using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Transform groundCheck;
    public Transform frontCheck;
    public float checkRadius;
    public float wallSlidingSpeed; 
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField]
    private LayerMask whatIsGround; 
    private bool isGrounded;
    private bool isTouchingFront;
    private bool wallSliding;
    private bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime; 

   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");


        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (horizontal < 0)
            sr.flipX = true;
        else if (horizontal > 0)
            sr.flipX = false;



        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.velocity = Vector2.up * jumpForce; 

        if (isTouchingFront && !isGrounded && horizontal != 0)
        {
            wallSliding = true; 
        }
        else
        {
            wallSliding = false; 
        }

        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue)); 
        }

        if (Input.GetButtonDown("Jump") && wallSliding == true)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime); 
        }

        if (wallJumping)
        {
            rb.velocity = new Vector2(xWallForce * -horizontal, yWallForce); 
        }


    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false; 
    }
}
