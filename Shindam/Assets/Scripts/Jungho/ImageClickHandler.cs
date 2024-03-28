using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 월드맵 각 맵 클릭시 씬 이동 구현 및 화면 전환 및 플레이어 스폰지점 설정 관련 스크립트
/// </summary>

public class ImageClickHandler : MonoBehaviour
{
    public string currentSceneName;
    public string targetSceneName;

    public float transitionTime = 1.0f;
    public Image blackScreen;
    public Vector3 playerSpawnPoint;
    [SerializeField]
    SceneController sceneController;

    private void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
        //현재 씬 이름 가져오기
        currentSceneName = SceneManager.GetActiveScene().name; // 수정해야함, MainScene가져옴
        // 초기에 어두운 이미지를 비활성화
        blackScreen.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        //클릭시 현재 씬이름과 다음 씬 이름이 달라야함
        if(currentSceneName != targetSceneName)
        {
            StartCoroutine(TransitionToScene());
        }
        
    }
    
    IEnumerator TransitionToScene()
    {
        // 클릭한 경우 어두운 이미지를 활성화합니다.
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


        StartCoroutine(sceneController.LoadScene(targetSceneName, LoadSceneMode.Additive));
        //Player의 스폰지점 설정
        PlayerAction player = FindObjectOfType<PlayerAction>(); 
        if (player != null)
        {
            player.transform.position = playerSpawnPoint;
        }
        // 씬 전환
        //SceneManager.LoadScene(targetSceneName);
        
        blackScreen.gameObject.SetActive(false);
    }

}
