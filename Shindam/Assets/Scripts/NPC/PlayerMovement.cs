using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float moveDirection;
    public float moveSpeed;
    Rigidbody2D playerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    } 

    private void FixedUpdate()
    {
        playerRigidbody.velocity = new Vector2(moveDirection * moveSpeed, playerRigidbody.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
    }
}
