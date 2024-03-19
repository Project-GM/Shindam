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
    [SerializeField] private ItemSO item; //보상으로 지급할 아이템
    [SerializeField] private GameObject actionMark;
    [SerializeField] private GameObject miniGameUI;
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private bool isPlayingMiniGame;
    //Inventory inventory;
    PlayerAction player;
    private void Start()
    {
        miniGameUI = GameObject.FindGameObjectWithTag("MiniGameUI");
        actionMark.SetActive(false);
        //inventory = FindAnyObjectByType<Inventory>();
        player = FindAnyObjectByType<PlayerAction>();
    }
    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E)) //상호작용 시
        {
            canInteract = false;
            actionMark.SetActive(false);
            StartCoroutine(MiniGame());
            Debug.Log(transform.position);
        }
        if(isPlayingMiniGame) miniGameUI.transform.position = Camera.main.WorldToScreenPoint(transform.position - new Vector3(0, 1f, 0));
    }
    private void OnTriggerEnter2D(Collider2D collision) //플레이어가 콜라이더 안에 들어오면 상호작용 가능
    {
        actionMark.SetActive(true); //UI 출력
        if (collision.gameObject.CompareTag("Player")) canInteract = true;
    }
        
    private void OnTriggerExit2D(Collider2D collision) //플레이어가 콜라이더 밖으로 나가면 상호작용 불가능
    {
        actionMark.SetActive(false); //UI 출력 안함
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
                inventoryData.AddItem(item, rewardCount);
                break;
            case 2: //11~24번, 보상 3~5개 랜덤 습득
                rewardCount = Random.Range(3, 6);
                inventoryData.AddItem(item, rewardCount);
                break;
            case 3: //25~30번, 보상 6~7개 랜덤 습득
                rewardCount = Random.Range(6, 8);
                inventoryData.AddItem(item, rewardCount);
                break;
        }
    }
    public int SetIndex(int clickCount) //테스트용 인덱스 설정 함수
    {
        if (clickCount == 0) return 0;
        else if (clickCount > 0 && clickCount <= 10) return 1;
        else if (clickCount > 10 && clickCount <= 24) return 2;
        else return 3;
    }
    public IEnumerator MiniGame() //테스트용 미니게임
    {
        isPlayingMiniGame = true;
        PlayerAction.s_Instance.StartInteracting();
        miniGameUI.transform.GetChild(0).gameObject.SetActive(true);
        miniGameUI.transform.GetChild(1).gameObject.SetActive(true);
        int clickCount = 0;
        while (miniGameUI.transform.GetChild(0).GetComponent<TimerUI>().isGaming)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                clickCount++;
            }
            yield return null;
        }
        RewardItem(SetIndex(clickCount));
        PlayerAction.s_Instance.EndInteracting();
        miniGameUI.transform.GetChild(0).gameObject.SetActive(false);
        miniGameUI.transform.GetChild(1).gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
        isPlayingMiniGame = false;
        Destroy(gameObject);
    }
}
