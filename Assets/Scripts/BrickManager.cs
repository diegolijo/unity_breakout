using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "ball")
        {
            // rb.AddForce(new Vector3(0, 1, 0), ForceMode2D.Impulse);
            // rb.bodyType = RigidbodyType2D.Dynamic;
            // rb.gravityScale = 1f;
        }

    }


}
