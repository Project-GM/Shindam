using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRender;

    public int playerSpeed = 1;
    public int jumpForce = 1;
    bool isJumping = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); 
        //anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)//&& !anim.GetBool("isJumping")
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            // 오른쪽 키를 눌렀고, 왼쪽 키를 누르지 않았을 때
            transform.Translate(new Vector3(playerSpeed * Time.deltaTime, 0, 0));
            spriteRender.flipX = false;
            //anim.SetBool("isRunning", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            // 왼쪽 키를 눌렀고, 오른쪽 키를 누르지 않았을 때
            transform.Translate(new Vector3(-playerSpeed * Time.deltaTime, 0, 0));
            spriteRender.flipX = true;
            
            //anim.SetBool("isRunning", true);
        }
        //else
        //{
        //    anim.SetBool("isRunning", false);
        //}
        Debug.DrawRay(rigid.position - new Vector2(0, 1f), Vector3.down, new Color(1, 0, 0));

        if (rigid.velocity.y < 0)
        {

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position - new Vector2(0, 1f), Vector3.down, 0.5f, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.01f)
                {
                    isJumping = false;
                    //anim.SetBool("isJumping", false);
                }

            }
        }

    }

    void Jump()
    {
        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJumping = true;
        //anim.SetBool("isJumping", true);
    }
}
