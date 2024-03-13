using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrewingTea : MonoBehaviour
{
    public int craftID;
    public GameObject miniGame1;
    public GameObject miniGame2;
    public bool isMiniGame1Finished = false;
    public List<bool> successOrFailure = new List<bool>();
    public CraftDBItem CraftDBItem = new CraftDBItem();
    private void Update()
    {
        if (isMiniGame1Finished)
        {
            Debug.Log("미니게임1 종료" + successOrFailure[0].ToString());
            //StartMiniGame_2();
        }
    }
    public void StartMiniGame_1()
    {
        miniGame1.SetActive(true);
    }
    public void StartMiniGame_2()
    {
        miniGame2.SetActive(true);
    }
}
