using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMove : MonoBehaviour
{
    public float interactionDistance = 1f; // ��ȣ�ۿ� ������ �Ÿ�
    public GameObject guiSprite; // ǥ���� GUI Sprite

    private void Update()
    {
        // �÷��̾ ���� �Ÿ� �ȿ� ���� �� GUI Sprite ǥ��
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
