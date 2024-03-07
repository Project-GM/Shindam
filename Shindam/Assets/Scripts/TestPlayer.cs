using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] Inventory inventory;
    Vector2 movement = new Vector2();
    Rigidbody2D rigidbody2D;
    private void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.enabled) inventory.CloseInventory();
            else inventory.OpenInventory();
        }
    }
    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        rigidbody2D.velocity = movement * moveSpeed;
    }
}
