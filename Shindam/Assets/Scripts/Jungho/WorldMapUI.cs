using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.TimeZoneInfo;

public class WorldMapUI : MonoBehaviour
{

    public MapButton teaHouseMapButton;
    public MapButton parkMapButton;
    public string currentSceneName;
    public string targetSceneName;


    void Awake()
    {
        
        teaHouseMapButton.onClickEvent += OnMapButtonClick;
        parkMapButton.onClickEvent += OnMapButtonClick;
    }

    // Update is called once per frame
    public void OnMapButtonClick(string sceneName)
    {
        currentSceneName = GameManager.instance.sceneManager.currentScene;
        targetSceneName = sceneName;
        // 씬 전환
        if (currentSceneName != targetSceneName)
        {
            StartCoroutine(GameManager.instance.sceneManager.LoadScene(targetSceneName, LoadSceneMode.Additive));
        }
    }

}
