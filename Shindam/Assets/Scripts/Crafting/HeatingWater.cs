using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatingWater : MonoBehaviour
{
    public Slider slider;
    public int speed = 3;
    public float minPos;
    public float maxPos;
    public RectTransform successRange;
    public Button heatingWaterButton;

    private void Start()
    {
        heatingWaterButton = gameObject.GetComponent<Button>();
    }
    public void StartMiniGame()
    {
        successRange.anchoredPosition = new Vector2(Random.Range(0, slider.maxValue - successRange.sizeDelta.x), 0);
        heatingWaterButton.interactable = false;
        slider.gameObject.SetActive(true);
        slider.value = 1;
        minPos = successRange.anchoredPosition.x;
        maxPos = successRange.sizeDelta.x + minPos;
        StartCoroutine(MiniGame());
    }
    IEnumerator MiniGame()
    {
        yield return null;
        int sign = 1;
        while (!(Input.GetKey(KeyCode.Space)))
        {
            if (slider.value == slider.maxValue) sign = -1;
            if (slider.value == 0) sign = 1;
            slider.value += Time.deltaTime * sign * speed;
            yield return null;
        }
        if(slider.value >= minPos && slider.value <= maxPos)
        {
            Debug.Log("성공");
        }
        else
        {
            Debug.Log("실패");
        }
        slider.gameObject.SetActive(false);
        heatingWaterButton.interactable = true;
    }
}
