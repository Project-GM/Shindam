using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BrewingTea : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{ 
    public int craftID;
    public GameObject miniGame1;
    public GameObject miniGame2;
    public bool isMiniGame1Finished = false;
    public bool isMiniGame2Finished = false;
    public bool isSuccess = false;
    public RectTransform teaCupTransform;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    private Vector2 originalPosition;
    [SerializeField]
    private CraftingSystem craftingSystem;
    private void OnEnable()
    {
        minX = teaCupTransform.anchoredPosition.x;
        maxX = minX + teaCupTransform.sizeDelta.x;
        minY = teaCupTransform.anchoredPosition.y;
        maxY = minY + teaCupTransform.sizeDelta.y;
        isMiniGame1Finished = false;
        isMiniGame2Finished = false;
        isSuccess = false;
    }
    public void StartMiniGame1()
    {
        miniGame1.SetActive(true);
    }
    public void StartMiniGame2()
    {
        miniGame2.SetActive(true);
        miniGame2.GetComponent<BrewingMiniGame_2>().StartMiniGame();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isMiniGame1Finished && isMiniGame2Finished)
        {
            originalPosition = transform.position;
            GetComponent<Image>().raycastTarget = false;
            transform.position = eventData.position;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isMiniGame1Finished && isMiniGame2Finished)
        {
            transform.position = eventData.position;
            if(transform.position.x > minX && transform.position.x < maxX && transform.position.y > minY && transform.position.y < maxY)
            {
                transform.position = originalPosition;
                GetComponent<Image>().raycastTarget = true;
                //차 따르는 애니메이션 실행
                IsSuccess();
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData) 
    {
        if (isMiniGame1Finished && isMiniGame2Finished)
        {
            GetComponent<Image>().raycastTarget = true;
            transform.position = originalPosition;
        }
    }
    public void IsSuccess()
    {
        craftingSystem.isSuccess = isSuccess;
        craftingSystem.FinishCrafting();
        GetComponent<Button>().interactable = true;
        gameObject.SetActive(false);
    }
}
