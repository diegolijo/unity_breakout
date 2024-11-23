using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallController : MonoBehaviour
{
    Dictionary<string, int> bricks = new Dictionary<string, int>
    {
        {"brick-b", 1},
        {"brick-o", 2},
        {"brick-g", 5},
        {"brick-y", 10},
    };
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
        if (tag == "player")
        {
            ContactPoint2D contacts = collision.GetContact(0);
            Debug.Log(tag + " contanct: " + contacts.point.x + "  position: " + transform.position.x + " diff: " + (transform.position.x - contacts.point.x));
            // fuerza necesaria para recuperar la velocidad actual desde el reposo
            Vector2 newForce = rb.velocity * rb.mass;
            rb.velocity = Vector2.zero;
            rb.AddForce(newForce * 1.0f, ForceMode2D.Impulse);
        }

        if (bricks.ContainsKey(tag))
        {
            Destroy(collision.gameObject);
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
