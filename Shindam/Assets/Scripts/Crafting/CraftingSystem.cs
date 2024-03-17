using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public GameObject brewingTea;
    public GameObject craftingUIBase;
    public bool isCrafting;
    public bool isSuccess = false;
    public void StartCrafting(int craftID)
    {
        isCrafting = true;
        brewingTea.GetComponent<BrewingTea>().craftID = craftID;
        craftingUIBase.SetActive(true);
        if (!brewingTea.activeSelf) brewingTea.SetActive(true);
    }
    public void FinishCrafting()
    {
        isCrafting = false;
        craftingUIBase.SetActive(false);
    }
}
