using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BrewingTea : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{ 
    public int craftID;
    public GameObject miniGame;
    public bool isMiniGame1Finished = false;
    public bool isSuccess;
    private Vector2 originalPosition;
    private void Update()
    {
        if (isMiniGame1Finished) GetComponent<Button>().interactable = false;
    }
    public void StartMiniGame()
    {
        miniGame.SetActive(true);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isMiniGame1Finished)
        {
            originalPosition = transform.position;
            GetComponent<Image>().raycastTarget = false;
            transform.position = eventData.position;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isMiniGame1Finished)
        {
            transform.position = eventData.position;
        }
    }
    public void OnEndDrag(PointerEventData eventData) 
    {
        if (isMiniGame1Finished)
        {
            GetComponent<Image>().raycastTarget = true;
            transform.position = originalPosition;
        }
    }
}
