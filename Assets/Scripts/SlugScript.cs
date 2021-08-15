using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugScript : MonoBehaviour
{
    public float speed;

    private float min;
    private float max;
    public Transform topCheck;
    public float checkRadius;
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] audioClips; 
    private bool isTouchingTop; 
    // Start is called before the first frame update
    void Start()
    {
        min = transform.position.x;
        max = transform.position.x + 3; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);


        isTouchingTop = Physics2D.OverlapCircle(topCheck.position, checkRadius, whatIsPlayer);

        if (isTouchingTop)
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
            Destroy(gameObject);
        }
           

    }
}
