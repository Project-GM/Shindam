using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.TimeZoneInfo;

public class SceneController : MonoBehaviour
{
    public Vector3 playerSpawnPoint;
    public float transitionTime = 1.0f;
    public CanvasGroup blackScreen;
    //public SpriteRenderer blackScreen;
    public string currentScene;
    public string StartScene;
    //public Dictionary<string, LoadSceneMode> loadScenes = new Dictionary<string, LoadSceneMode>();
    public List<string> loadScenes = new List<string>();
    //int,string으로 scene이름으로 관리
    void InitSceneInfo()
    {
        //loadScenes.Add("TeaHouse1",LoadSceneMode.Additive);
        //loadScenes.Add("Park1", LoadSceneMode.Additive);
    }

    void Start()
    {
        blackScreen.gameObject.SetActive(false);
        InitSceneInfo();
        SceneManager.LoadScene(StartScene, LoadSceneMode.Additive);
        currentScene = StartScene;
        playerSpawnPoint = new Vector3(-31.74f, -2.14f, 0);
    }

    public IEnumerator LoadScene(string sceneName, LoadSceneMode mode)
    {
        //blackScreen.gameObject.SetActive(true);
        //float timeElapsed = 0f;
        //float alpha = 0f;

        //while (timeElapsed < transitionTime)
        //{

        //    timeElapsed += Time.deltaTime;
        //    alpha = Mathf.Clamp01(timeElapsed / transitionTime);
        //    blackScreen.color = new Color(0f, 0f, 0f, alpha);
        //    yield return null;
        //}

        //while (blackScreen.alpha < 1)
        //{
        //    blackScreen.alpha += Time.deltaTime;
        //    yield return null;
        //}

        if (sceneName == loadScenes[0])
        {
            SceneManager.UnloadSceneAsync(currentScene);
            currentScene = loadScenes[0];
            playerSpawnPoint = new Vector3(-31.74f, -2.14f, 0);
        }
        else if (sceneName == loadScenes[1])
        {
            SceneManager.UnloadSceneAsync(currentScene);
            currentScene = loadScenes[1];
            playerSpawnPoint = new Vector3(25f, -1f, 0);
 
        }
        PlayerAction player = FindObjectOfType<PlayerAction>();
        
        if (player != null)
        {
            player.transform.position = playerSpawnPoint;
        }

        blackScreen.gameObject.SetActive(false);
        SceneManager.LoadScene(sceneName, mode);

        yield return null;
    }
}
