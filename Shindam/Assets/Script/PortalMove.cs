using System.Collections;
using UnityEngine;

/// <summary>
/// 포탈에 플레이어가 가까이 갈시 월드맵 gui창 관련 스크립트
/// </summary>

public class PortalMove : MonoBehaviour
{
    public float interactionDistance = 1f; // 상호작용 가능한 거리
    public GameObject guiSprite; // 표시할 GUI Sprite
    private bool isEscPressed = false; //ESC키가 눌렸는지 안눌렸는지
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isEscPressed = true;
            guiSprite.SetActive(false);
        }

        // 플레이어가 일정 거리 안에 있을 때 GUI Sprite 표시
        if (IsPlayerNearby()&&!isEscPressed)
        {
            Interact();
            guiSprite.SetActive(true);
            // GUI Sprite 활성화
        }
        else if(!IsPlayerNearby())//거리안에 없을시
        {
            // GUI Sprite 비활성화
            guiSprite.SetActive(false);
            isEscPressed=false;
        }
    }

    private bool IsPlayerNearby()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            return distance <= interactionDistance;
        }

        return false;
    }

    private void Interact()
    {
        // 상호작용 함수
        // 이 부분에 포탈과 상호작용하는 내용을 추가하면 됩니다.
        // 예를 들어, 포탈로 이동하는 코드를 작성할 수 있습니다.
        Debug.Log("Interacting with portal!");

        // GUI Sprite 비활성화
        guiSprite.SetActive(false);

        // 다른 장소로 이동
        // SceneManager.LoadScene("다음 장소의 씬 이름");
    }
}