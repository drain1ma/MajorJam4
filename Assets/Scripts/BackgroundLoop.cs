using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    [Range(-1f, 1f)]
    private float offset;
    private Material mat;
    public GameObject player;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
    private void Update()
    {
        float input = Input.GetAxis("Horizontal");
        if (player.GetComponent<Rigidbody2D>().velocity.magnitude > 0 && player.GetComponent<Rigidbody2D>().velocity.y == 0)
            offset += (Time.deltaTime * input) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}