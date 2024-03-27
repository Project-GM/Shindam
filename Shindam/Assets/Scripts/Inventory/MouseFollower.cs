using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 드래그 슬롯 관리하는 함수
/// </summary>
public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas; //UI 캔버스
    [SerializeField]
    private UIInventoryItem item; //슬롯 하나
    public void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>(); 
        item = GetComponentInChildren<UIInventoryItem>();
    }
    public void SetData(Sprite sprite, int quantity) //슬롯에 아이템 등록
    {
        item.SetData(sprite, quantity);
    }
    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Input.mousePosition,
            canvas.worldCamera,
            out position
            );
        transform.position = canvas.transform.TransformPoint(position); //마우스 포인터에 드래그 슬롯 위치 고정
    }
    public void Toggle(bool val) //드래그 슬롯 활성화 비활성화 함수
    {
        Debug.Log($"Item toggled {val}");
        gameObject.SetActive( val );
    }
}
