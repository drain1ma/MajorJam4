using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed; 
    private Rigidbody2D rb;
    private SpriteRenderer sr; 
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
        else
            sr.flipX = false; 


    }
}
