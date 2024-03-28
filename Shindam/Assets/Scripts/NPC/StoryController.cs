using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    public bool isNpcSpawned = false;
    public GameObject storyNpc;

    void Update()
    {
        if(!isNpcSpawned && GameManager.instance.timeManager.GetTime() == GameTime.night)
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
