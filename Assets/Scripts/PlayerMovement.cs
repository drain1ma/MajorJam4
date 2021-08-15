using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Transform groundCheck;
    public Transform[] frontCheck;
    public float checkRadius;
    public float wallSlidingSpeed; 
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [SerializeField]
    private GameObject desertUI;
    [SerializeField]
    private Text numOfDeserts;
    private int desertTotal; 
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] audioClips;
    [SerializeField]
    private GameObject black; 
    private bool isGrounded;
    private bool isTouchingFront;
    private bool isTouchingFront2;
    private bool wallSliding;
    private bool wallJumping;
    private bool showingUI; 
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime; 

   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        showingUI = true;
        desertTotal = 0; 
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
        isTouchingFront = Physics2D.OverlapCircle(frontCheck[0].position, checkRadius, whatIsGround);
        isTouchingFront2 = Physics2D.OverlapCircle(frontCheck[1].position, checkRadius, whatIsGround);
        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.velocity = Vector2.up * jumpForce; 

        if (isTouchingFront && !isGrounded && horizontal != 0)
        {
            wallSliding = true; 
        }
        else if (isTouchingFront2 && !isGrounded && horizontal != 0)
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


        if (Input.GetButtonDown("Cancel") && showingUI)
        {
            Time.timeScale = 0; 
            desertUI.SetActive(true);
            showingUI = false;
            black.SetActive(true);
        }
        else if (Input.GetButtonDown("Cancel") && !showingUI)
        {
            Time.timeScale = 1; 
            desertUI.SetActive(false);
            showingUI = true;
            black.SetActive(false); 
        }


    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false; 
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Desert"))
        {
            desertTotal++;
            numOfDeserts.text = desertTotal.ToString();
            audioSource.clip = audioClips[0];
            audioSource.Play(); 
            Destroy(collision.gameObject); 
        }
    }
}
