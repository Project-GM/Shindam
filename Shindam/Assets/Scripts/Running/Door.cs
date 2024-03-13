using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.TimeZoneInfo;

public class Door : MonoBehaviour
{
    public Collider2D doorCollider;
    public Transform playerTransform;
    public Transform cameraTransform;
    public Transform spawnPointTransform;
    public Image blackScreen;
    public float fadeDuration = 1f;

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(teleport());
        }
    }

    IEnumerator teleport()
    {
        // 클릭한 경우 어두운 이미지를 활성화합니다.
        blackScreen.gameObject.SetActive(true);

        float timeElapsed = 0f;
        float alpha = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            alpha = Mathf.Clamp01(timeElapsed / fadeDuration);
            blackScreen.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        // 이동 로직 (플레이어 위치 변경)
        playerTransform.position = spawnPointTransform.position;
        cameraTransform.position = spawnPointTransform.position + new Vector3(0, 2, 0);

        // 화면을 페이드 아웃
        blackScreen.gameObject.SetActive(false);


        yield return null;
    }
}
