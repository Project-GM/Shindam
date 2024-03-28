using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject timeManager;
    public GameObject dialogueManager;
    public GameObject dialogueDataManager;
    public GameObject customerManager;
    public SceneController sceneManager;
    public GameObject runningManager;
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
