using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ����� �� �� Ŭ���� �� �̵� ���� �� ȭ�� ��ȯ �� �÷��̾� �������� ���� ���� ��ũ��Ʈ
/// </summary>

public class ImageClickHandler : MonoBehaviour
{
    public string currentSceneName;
    public string targetSceneName;
    public float transitionTime = 1.0f;
    public Image blackScreen;
    public Vector3 playerSpawnPoint;

    private void Start()
    {
        //���� �� �̸� ��������
        currentSceneName = SceneManager.GetActiveScene().name;
        // �ʱ⿡ ��ο� �̹����� ��Ȱ��ȭ
        blackScreen.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        //Ŭ���� ���� ���̸��� ���� �� �̸��� �޶����
        if(currentSceneName != targetSceneName)
        {
            StartCoroutine(TransitionToScene());
        }
        
    }
    
    IEnumerator TransitionToScene()
    {
        // Ŭ���� ��� ��ο� �̹����� Ȱ��ȭ�մϴ�.
        blackScreen.gameObject.SetActive(true);

        float timeElapsed = 0f;
        float alpha = 0f;

        while (timeElapsed < transitionTime)
        {
            timeElapsed += Time.deltaTime;
            alpha = Mathf.Clamp01(timeElapsed / transitionTime);
            blackScreen.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
        

        //Player�� �������� ����
        PlayerAction player = FindObjectOfType<PlayerAction>();
        if (player != null)
        {
            player.transform.position = playerSpawnPoint;
        }
        // �� ��ȯ
        SceneManager.LoadScene(targetSceneName);

    }
}
