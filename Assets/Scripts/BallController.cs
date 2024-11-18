using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class BallController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] GameManager manager;
    [SerializeField] float force;
    [SerializeField] float MAX_DEG = 30f;
    public Vector3 resetPosition;
    private int delay = 1;

    void Start()
    {
        force = 5f;
        resetPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(LaunchBall());
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        Debug.Log(tag);
        if (tag == "bottom")
        {

        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.tag;

        if (tag == "bottom")
        {

            StartCoroutine(LaunchBall());
        }
    }


    IEnumerator LaunchBall()
    {
        yield return new WaitForSeconds(delay);
        transform.position = new Vector3(resetPosition.x, resetPosition.y, 0);
        float deg = Random.Range(0, MAX_DEG) * Mathf.Deg2Rad;
        int dirX = new[] { -1, 1 }[Random.Range(0, 2)];
        float x = Mathf.Sin(deg) * dirX;
        float y = Mathf.Cos(deg) * -1;
        Vector2 impulse = new Vector2(x, y);
        rb.velocity = Vector2.zero;
        rb.AddForce(impulse * force, ForceMode2D.Impulse);
    }
}
