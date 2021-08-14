using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugScript : MonoBehaviour
{
    public float speed;

    private float min;
    private float max; 

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
    }
}
