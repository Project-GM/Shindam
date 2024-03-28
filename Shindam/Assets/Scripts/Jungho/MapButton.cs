using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;

public class MapButton : MonoBehaviour
{
    public string targetScene;
    public event Action<string> onClickEvent;
    // Start is called before the first frame update
    public void OnButtonClick()
    {
        // 클릭 이벤트 발생 ? -> null이 아닐시
        onClickEvent?.Invoke(targetScene);
    }
}
