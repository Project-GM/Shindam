using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMove : MonoBehaviour
{
    public float customerSpeed = 1f;
    SpriteRenderer spriteRenderer;

    private bool isFinish = false;
    private bool isSitted = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (isFinish)
        {
            moveLeft();
        }
        else if (isSitted)
        {
            moveStop();
        }
        moveRight();
    }

    void moveRight()
    {
        transform.Translate(new Vector3(customerSpeed * Time.deltaTime, 0, 0));
        spriteRenderer.flipX = false;
    }

    void moveLeft()
    {
        transform.Translate(new Vector3(-1f * customerSpeed * Time.deltaTime, 0, 0));
        spriteRenderer.flipX = true;
    }

    void moveStop()
    {

    }
}
