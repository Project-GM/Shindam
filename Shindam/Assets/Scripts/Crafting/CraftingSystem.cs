using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public GameObject brewingTea;
    private void OnEnable()
    {
        if(!brewingTea.activeSelf) brewingTea.SetActive(true);
    }
    public void FinishCrafting()
    {
        gameObject.SetActive(false);
    }
}
