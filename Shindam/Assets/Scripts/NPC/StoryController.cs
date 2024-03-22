using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    public bool isNpcSpawned = false;
    public GameObject storyNpc;

    void Update()
    {
        if(!isNpcSpawned && TimeManager.instance.GetTime() == "저녁")
        {
            isNpcSpawned = true;
            spawnNpc();
        }

        if(DialogueManager.instance.isDialogueFinish == true)
        {
            storyNpc.SetActive(false);
        }
    }

    private void spawnNpc()
    {
        storyNpc.SetActive(true);
    }
}
