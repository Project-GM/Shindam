using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TimeManager timeManager;
    public DialogueManager dialogueManager;
    public GameObject dialogueDataManager;
    public CustomerManager customerManager;
    public SceneController sceneManager;
    public RunningManager runningManager;
    public MapController mapManager;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
