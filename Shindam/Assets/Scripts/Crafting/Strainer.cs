using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Strainer : MonoBehaviour, IDropHandler
{
    public List<Image> itemImages = new List<Image>();
    [Range(0,3)]public int ingredientCount;
    public List<Item> itemList;
    public CanvasGroup ingredientFullFloating;
    private bool isFloatingMessage;

    private void OnEnable()
    {
        ingredientFullFloating.alpha = 0;
        for(int i = 0; i < itemImages.Count; i++)
        {
            SetColor(0, itemImages[i]);
        }
        ingredientCount = 0;
        itemList = new List<Item>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(ingredientCount == 3)
        {
            if(!isFloatingMessage) StartCoroutine(EnableIngredientFullFloating());
            return;
        }
        if(DragSlot.instance.dragSlot != null)
        {
            DragSlot.instance.dragSlot.itemCount--;
            DragSlot.instance.dragSlot.item = DragSlot.instance.dragSlot.item;
            itemList.Add(DragSlot.instance.dragSlot.item);
            SetColor(1, itemImages[ingredientCount]);
            itemImages[ingredientCount].sprite = DragSlot.instance.itemImage.sprite;
            ingredientCount++;
        }
    }
    public void SetColor(float alpha, Image itemImage)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }
    IEnumerator EnableIngredientFullFloating()
    {
        isFloatingMessage = true;
        ingredientFullFloating.gameObject.SetActive(true);
        while (ingredientFullFloating.alpha < 1)
        {
            ingredientFullFloating.alpha += Time.deltaTime * 1.5f;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        while (ingredientFullFloating.alpha > 0)
        {
            ingredientFullFloating.alpha -= Time.deltaTime * 1.5f;
            yield return null;
        }
        ingredientFullFloating.gameObject.SetActive(false);
        isFloatingMessage = false;
    }
}
