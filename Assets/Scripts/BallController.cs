using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallController : MonoBehaviour
{
    Dictionary<string, int> bricks = new Dictionary<string, int>
    {
        {"brick-r", 10},
        {"brick-o", 5},
        {"brick-g", 2},
        {"brick-y", 1},
    };
    Rigidbody2D rb;
    AudioSource aSource;
    [SerializeField] GameManager game;
    [SerializeField] float initialForce = 5f;
    [SerializeField] float MAX_DEG = 30f;
    public Vector3 resetPosition;
    private int delay = 1;

    [SerializeField] AudioClip fxPadle;
    [SerializeField] AudioClip fxBrick;
    [SerializeField] AudioClip fxWall;
    [SerializeField] AudioClip fxFail;

    int countColisionPlayer = 0;
    [SerializeField] float incForce = 1f;
    Vector3 playerScale;
    GameObject player;

    void Start()
    {
        StartCoroutine(LaunchBall());

        player = GameObject.FindWithTag("player");
        playerScale = player.transform.localScale;

        resetPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        aSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "wall-side" || tag == "wall-top")
        {
            aSource.clip = fxWall;
            aSource.Play();
            if (tag == "wall-top")
            {
                Vector3 scale = playerScale;
                scale.x *= 0.5f;
                player.transform.localScale = scale;
            }
        }
        if (tag == "player")
        {
            aSource.clip = fxPadle;
            aSource.Play();
            ContactPoint2D contact = collision.GetContact(0);
            float diff = collision.gameObject.transform.position.x - contact.point.x;
            // fuerza necesaria para recuperar la velocidad actual desde el reposo
            Vector2 currentForce = rb.velocity * rb.mass;
            // cambio de sentido de la pelota
            if ((rb.velocity.x > 0 && diff > 0) || (rb.velocity.x < 0 && diff < 0))
            {
                currentForce.x *= -1;
            }
            // aumentar velocidad pelota
            countColisionPlayer += 1;
            if (countColisionPlayer % 4 == 0)
            {
                currentForce = currentForce * (1.0f + incForce);
                Debug.Log("add force" + (1.0f + incForce));
            }
            rb.velocity = Vector2.zero;
            rb.AddForce(currentForce, ForceMode2D.Impulse);
        }

        if (bricks.ContainsKey(tag))
        {
            aSource.clip = fxBrick;
            aSource.Play();
            Debug.Log("" + tag + "");
            game.AddScore(bricks[tag]);
            Destroy(collision.gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.tag;
        if (tag == "bottom")
        {
            aSource.clip = fxFail;
            aSource.Play();
            game.consumeLife();
            countColisionPlayer = 0;
            player.transform.localScale = playerScale;
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
        rb.AddForce(impulse * initialForce, ForceMode2D.Impulse);
    }
}
