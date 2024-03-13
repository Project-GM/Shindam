using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터 관련 스크립트 ( 움직임, 씬이동시 객체 유지 등)
/// </summary>

public class PlayerAction : MonoBehaviour
{

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRender;

    public int playerSpeed = 1;
    public int jumpForce = 1;
    public bool isJumping = false;
    public bool isInteracting = false;


    private static PlayerAction s_Instance = null;
        
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); 
        //anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInteracting) //상호작용 안할시만 움직임
        {
            Move();
        }
        //InputKey();
    }
    void Awake()
    {
        //player 씬 이동시 객체 유지
        if (s_Instance)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        s_Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Move()
    {
        //Player 움직임 관련 함수
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
            if (rayHit.collider != null && rayHit.distance < 0.1f) // 충돌되면
            {

                isJumping = false;
                //anim.SetBool("isJumping", false);

            }
        }
        else if (rigid.velocity.y == 0)
        {
            // 플레이어가 지면에 착지했을 때
            isJumping = false;
        }

    }
   
    void Jump() //player jump 함수
    {
        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJumping = true;
        //anim.SetBool("isJumping", true);
    }

/*    void InputKey()
    {
        //play key 입력시 이벤트
        switch (Event.current.keyCode)
        {
            case KeyCode.E: //E키 누를시 상호작용 함수
                
                break;
            default:
              
                break;
        }
    }*/

    void StartInteracting()
    {
        //상호작용 시작시 호출
        isInteracting = true;
    }

    void EndInteracting()
    {
        //상호작용 끝날시 호출
        isInteracting = false;

    }

}
