using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    Vector2 playerMove;
    public int moveSpeed = 5;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        playerMove.x = Input.GetAxisRaw("Horizontal");
        playerMove.y = Input.GetAxisRaw("Vertical");
        playerMove.Normalize();
        rb.velocity = playerMove * moveSpeed;
    }
}
