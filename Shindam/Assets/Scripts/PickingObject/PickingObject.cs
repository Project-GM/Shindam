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
    private void Awake()
    {
        actionMark.SetActive(false);
    }
    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E) && !PlayerAction.s_Instance.isJumping) //상호작용 시
        {
            canInteract = false;
            actionMark.SetActive(false);
            GameManager.instance.miniGameManager.OnEndButtonMashingMiniGame += RewardItem;
            GameManager.instance.miniGameManager.StartButtonMashingMiniGame(transform.position);
        }
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
        GameManager.instance.miniGameManager.OnEndButtonMashingMiniGame -= RewardItem;
        Destroy(gameObject);
    }
}
