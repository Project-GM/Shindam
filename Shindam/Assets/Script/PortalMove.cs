using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMove : MonoBehaviour
{
    public float interactionDistance = 1f; // 상호작용 가능한 거리
    public GameObject guiSprite; // 표시할 GUI Sprite

    private void Update()
    {
        // 플레이어가 일정 거리 안에 있을 때 GUI Sprite 표시
        if (IsPlayerNearby())
        {
            guiSprite.SetActive(true);
        }
        else
        {
            guiSprite.SetActive(false);
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
}
