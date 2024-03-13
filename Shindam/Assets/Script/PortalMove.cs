using System.Collections;
using UnityEngine;

/// <summary>
/// ��Ż�� �÷��̾ ������ ���� ����� guiâ ���� ��ũ��Ʈ
/// </summary>

public class PortalMove : MonoBehaviour
{
    public float interactionDistance = 1f; // ��ȣ�ۿ� ������ �Ÿ�
    public GameObject guiSprite; // ǥ���� GUI Sprite
    private bool isEscPressed = false; //ESCŰ�� ���ȴ��� �ȴ��ȴ���
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isEscPressed = true;
            guiSprite.SetActive(false);
        }

        // �÷��̾ ���� �Ÿ� �ȿ� ���� �� GUI Sprite ǥ��
        if (IsPlayerNearby()&&!isEscPressed)
        {
            Interact();
            guiSprite.SetActive(true);
            // GUI Sprite Ȱ��ȭ
        }
        else if(!IsPlayerNearby())//�Ÿ��ȿ� ������
        {
            // GUI Sprite ��Ȱ��ȭ
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
        // ��ȣ�ۿ� �Լ�
        // �� �κп� ��Ż�� ��ȣ�ۿ��ϴ� ������ �߰��ϸ� �˴ϴ�.
        // ���� ���, ��Ż�� �̵��ϴ� �ڵ带 �ۼ��� �� �ֽ��ϴ�.
        Debug.Log("Interacting with portal!");

        // GUI Sprite ��Ȱ��ȭ
        guiSprite.SetActive(false);

        // �ٸ� ��ҷ� �̵�
        // SceneManager.LoadScene("���� ����� �� �̸�");
    }
}