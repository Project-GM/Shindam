using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomerMove : MonoBehaviour
{
    public float customerSpeed = 1f;
   

    SpriteRenderer spriteRenderer;

    private bool isFinish = false;
    private bool isSat = false;
    private bool isOrdering = false;
    private bool isDrinking = false;
    private GameObject door;
    private GameObject seat;
    private GameObject[] chairs;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        FindSeat();
        door = GameObject.Find("Door(in)");
    }

    void Update()
    {
        if (isFinish)
        {
            MoveLeft();
        }
        else if (isSat)
        {
            MoveStop();
        }
        else MoveRight();
    }

    void FindSeat()
    {
        /*transform.Translate(new Vector3(customerSpeed * Time.deltaTime, 0, 0));
        spriteRenderer.flipX = false;
        if (Vector3.Distance(transform.position, chair[CustomerManager.instance.spawnPoint.transform.childCount - 1].position) < 0.01f)
        {
            isSat = true; //손님 의자에 앉음
            Debug.Log("앉았어요");
        }*/
        chairs = (GameObject.FindGameObjectsWithTag("Chair"));
        Debug.Log("의자 넣음");

        for (int i = 0; i < 3; i++)
        {
            if (!chairs[i].transform.GetComponent<Chair>().isFilled)
            {
                chairs[i].transform.GetComponent<Chair>().isFilled = true;
                seat = chairs[i];
                Debug.Log("자리 찾음");
                break;
            }
        }
    }

    void MoveRight()
    {
        transform.position = Vector2.MoveTowards(transform.position, seat.transform.position, customerSpeed / 30f);
        if(Vector2.Distance(transform.position, seat.transform.position) < 0.1f)
        {
            isSat = true;
        }
    }

    void MoveLeft()
    {
        transform.position = Vector2.MoveTowards(transform.position, door.transform.position, customerSpeed / 30f);
        spriteRenderer.flipX = true;
        if (Vector3.Distance(transform.position, door.transform.position) < 0.01f)
        {
            Destroy(gameObject); // 손님이 퇴장 지점에 도달하면 객체 파괴
        }
    }

    void MoveStop()
    {
        if (!isOrdering)
        {
            isOrdering = true;
            Order();
            Debug.Log("주문할게요");
        }
    }

    void Order()
    {
        //주문
        //미니게임 시작
        //미니게임 끝
        if (!isDrinking)
        {
            StartCoroutine(DrinkingTea());
        }
    }

    IEnumerator DrinkingTea()
    {
        yield return new WaitForSeconds(10f);
        isFinish = true;
        Debug.Log("잘 마셨습니다");
        seat.GetComponent<Chair>().isFilled = false;
    }
}
