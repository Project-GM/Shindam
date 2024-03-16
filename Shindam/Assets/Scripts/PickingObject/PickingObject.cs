using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 채집물 스크립트
/// </summary>
public class PickingObject : MonoBehaviour
{    
    [SerializeField] private bool canInteract = false; //상호작용 가능 여부
    [SerializeField] private InterationFloating interactionFloating;//상호작용 키 UI
    [SerializeField] private Item item; //보상으로 지급할 아이템
    [SerializeField] private GameObject testText;//테스트용 텍스트
    Inventory inventory;
    PlayerAction player;
    private void Start()
    {
        interactionFloating = FindAnyObjectByType<InterationFloating>();
        testText = GameObject.FindGameObjectWithTag("TestText");
        inventory = FindAnyObjectByType<Inventory>();
        player = FindAnyObjectByType<PlayerAction>();
    }
    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E)) //상호작용 시
        {
            canInteract = false;
            player.isInteracting = true;
            StartCoroutine(MiniGameTest());
            testText.GetComponent<Text>().text = ("미니게임 시작!");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //플레이어가 콜라이더 안에 들어오면 상호작용 가능
    {
        interactionFloating.EnableFloatingImage(transform.position + new Vector3(0, 0.8f, 0)); //UI 출력
        if (collision.gameObject.CompareTag("Player")) canInteract = true;
    }
        
    private void OnTriggerExit2D(Collider2D collision) //플레이어가 콜라이더 밖으로 나가면 상호작용 불가능
    {
        interactionFloating.DisableFloatingImage(); //UI 출력 안함
        if (collision.gameObject.CompareTag("Player")) canInteract = false;
    }
    public void RewardItem(int num) //보상 아이템 지급
    {
        int rewardCount = 0;
        switch (num)
        {
            case 0: //0번, 보상 없음
                break;
            case 1: //1~10번, 보상 1~2개 랜덤 습득
                rewardCount = Random.Range(1, 3);
                inventory.AddItem(item, rewardCount);
                break;
            case 2: //11~24번, 보상 3~5개 랜덤 습득
                rewardCount = Random.Range(3, 6);
                inventory.AddItem(item, rewardCount);
                break;
            case 3: //25~30번, 보상 6~7개 랜덤 습득
                rewardCount = Random.Range(6, 8);
                inventory.AddItem(item, rewardCount);
                break;
        }
        testText.GetComponent<Text>().text = ("보상 지급: " + rewardCount.ToString() + "개");
    }
    public int SetIndex(int clickCount) //테스트용 인덱스 설정 함수
    {
        if (clickCount == 0) return 0;
        else if (clickCount > 0 && clickCount <= 10) return 1;
        else if (clickCount > 10 && clickCount <= 24) return 2;
        else return 3;
    }
    public IEnumerator MiniGameTest() //테스트용 미니게임
    {
        int clickCount = 0;
        float time = 0f;
        while (time < 1.0f)
        {
            time += Time.deltaTime / 7f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                clickCount++;
                testText.GetComponent<Text>().text = clickCount.ToString();
            }
            yield return null;
        }
        RewardItem(SetIndex(clickCount));
        yield return new WaitForSeconds(2);
        testText.GetComponent<Text>().text = "";
        Destroy(gameObject);
        transform.parent.gameObject.SetActive(false);
        player.isInteracting = false;
    }
}
