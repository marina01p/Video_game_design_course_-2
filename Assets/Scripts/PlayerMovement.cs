using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // mișcare axa orizontală
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);

        // mișcare axa verticală
        float moveVertical = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(rb.velocity.x, moveVertical * moveSpeed);
    }
}