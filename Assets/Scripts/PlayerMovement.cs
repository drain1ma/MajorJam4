using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField]
    private GameObject[] doors;

    [SerializeField]
    private GameObject openDoor; 
    private bool isGrounded;
    private bool isTouchingFront;
    private bool isTouchingFront2;
    private bool wallSliding;
    private bool wallJumping;
    private bool showingUI;
    private bool canOpenDoor; 
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

        if (transform.position.y < -80)
        {
            SceneManager.LoadScene("Level1"); 
        }
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
        {
            rb.velocity = Vector2.up * jumpForce;
        }
            

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

        if (Input.GetButton("Fire2") && !isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject[] slugs = GameObject.FindGameObjectsWithTag("Slug");
            audioSource.clip = audioClips[1];
            audioSource.Play();
            foreach (GameObject slug in slugs)
            {
                Destroy(slug); 
            }
        }

        if (Input.GetButtonDown("Fire3") && canOpenDoor)
        {
            GameObject closestDoor = GetClosestDoor(doors);

            if (closestDoor.Equals(doors[0]) && canOpenDoor)
                transform.position = doors[1].transform.position;
            else if (closestDoor.Equals(doors[1]) && canOpenDoor)
                transform.position = doors[0].transform.position; 
        }
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false; 
    }

    GameObject GetClosestDoor(GameObject[] doors)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject d in doors)
        {
            float dist = Vector3.Distance(d.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = d;
                minDist = dist;
            }
        }
        return tMin;
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

        if (collision.gameObject.CompareTag("Door"))
        {
            openDoor.SetActive(true);
            canOpenDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            openDoor.SetActive(false);
            canOpenDoor = false;
        }
    }
}
