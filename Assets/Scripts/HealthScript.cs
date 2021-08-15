using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class HealthScript : MonoBehaviour
{
    public int health;
    public int numOfHearts;
    public float hitForce; 
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private Rigidbody2D rb; 
    [SerializeField]
    private GameObject[] enemies; 
    // Start is called before the first frame update
    [SerializeField]
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (health > numOfHearts)
            health = numOfHearts; 

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart; 

            if (i < numOfHearts)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Slug"))
        {
            health--;
            rb.AddForce(new Vector3(-hitForce, rb.velocity.y));
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Heart"))
        {
            health++;
            Destroy(collision.gameObject); 
        }
    }
}
