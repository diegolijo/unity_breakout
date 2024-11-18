using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float MAX_X = 3.25f;
    float MIN_X = -3.25f;
    [SerializeField] float speed;
    void Start()
    {
        speed = 10f;
    }

    void Update()
    {
        if (Input.GetKey("left"))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey("right"))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        Vector3 newPosition = transform.position;
        if (transform.position.x > MAX_X)
        {
            newPosition.x = MAX_X;
            transform.position = newPosition;
        }
        if (transform.position.x < MIN_X)
        {
            newPosition.x = MIN_X;
            transform.position = newPosition;
        }
    }
}
