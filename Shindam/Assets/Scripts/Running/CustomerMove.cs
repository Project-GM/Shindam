using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMove : MonoBehaviour
{
    public float customerSpeed = 1f;
    public GameObject door;

    SpriteRenderer spriteRenderer;
    private bool isFinish = false;
    private bool isSitted = false;
    private Transform chair;

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
        if (Vector3.Distance(transform.position, chair.position) < 0.1f)
        {
            isSitted = true; //손님 의자에 앉음
        }

    }

    void moveLeft()
    {
        transform.Translate(new Vector3(-1f * customerSpeed * Time.deltaTime, 0, 0));
        spriteRenderer.flipX = true;
        if (Vector3.Distance(transform.position, door.transform.position) < 0.1f)
        {
            Destroy(gameObject); // 손님이 퇴장 지점에 도달하면 객체 파괴
        }
    }

    void moveStop()
    {
        Order();
    }

    void Order()
    {
        //일단 녹차만 주문할게요

    }
}
