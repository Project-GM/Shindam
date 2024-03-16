using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomerMove : MonoBehaviour
{
    public float customerSpeed = 1f;
   

    SpriteRenderer spriteRenderer;

    public float detectionRange = 1f;
    private bool isFinish = false;
    private bool isSat = false;
    private bool isOrdering = false;
    private bool isDrinking = false;
    private GameObject door;
    private GameObject seat;
    private GameObject[] chairs;
    [SerializeField] CraftDB CraftDB;
    //[SerializeField] List<GameObject> orderBubbles;
    [SerializeField] CraftingSystem craftingSystem;
    private Transform playerTransform;
    int orderID;
    


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        craftingSystem = GameObject.FindGameObjectWithTag("CraftingUI").GetComponent<CraftingSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        FindSeat();
        door = GameObject.Find("Door(in)");
    }

    void Update()
    {
        if(isOrdering)
        {
            //orderBubble.SetActive(true);
            //orderBubble.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1f, 0));
        }
        else
        {
            //orderBubble.SetActive(false);
        }
    }
    private void FixedUpdate()
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
        List<int> indexList = new List<int>();
        int index;
        chairs = (GameObject.FindGameObjectsWithTag("Chair"));
        Debug.Log("의자 넣음");
        //의자 랜덤으로 앉는거 좀 찾아봅시다..
        for (int i = 0; i < 3; i++)
        {
            if (!chairs[i].transform.GetComponent<Chair>().isFilled)
            {
                indexList.Add(i);
            }
        }
        index = Random.Range(0, indexList.Count);
        chairs[indexList[index]].transform.GetComponent<Chair>().isFilled = true;
        seat = chairs[indexList[index]];
        Debug.Log("자리 찾음");
    }

    void MoveRight()
    {
        transform.position = Vector2.MoveTowards(transform.position, seat.transform.position, customerSpeed / 10f);
        if(Vector2.Distance(transform.position, seat.transform.position) < 0.3f)
        {
            isSat = true;
        }
    }

    void MoveLeft()
    {
        transform.position = Vector2.MoveTowards(transform.position, door.transform.position, customerSpeed / 10f);
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
            StartCoroutine(Order());
            Debug.Log("주문할게요");
        }
    }

    IEnumerator Order()
    {
        //주문
        orderID = CraftDB.items[Random.Range(0,CraftDB.items.Count)].ID;
        while (true)
        {
            if (IsinRange() && Input.GetKeyDown(KeyCode.E)) break;
            yield return null;
        }
        //미니게임 시작
        craftingSystem.StartCrafting(orderID);
        PlayerAction.s_Instance.isInteracting = true;
        while(craftingSystem.isCrafting) yield return null;
        //미니게임 끝
        PlayerAction.s_Instance.isInteracting = false;
        if (!isDrinking && craftingSystem.isSuccess)
        {
            StartCoroutine(DrinkingTea());
        }
        else
        {
            Debug.Log("불만족");
            isFinish = true;
            seat.GetComponent<Chair>().isFilled = false;
        }
    }

    IEnumerator DrinkingTea()
    {
        yield return new WaitForSeconds(10f);
        isFinish = true;
        Debug.Log("잘 마셨습니다");
        seat.GetComponent<Chair>().isFilled = false;
    }
    private bool IsinRange()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) <= detectionRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
