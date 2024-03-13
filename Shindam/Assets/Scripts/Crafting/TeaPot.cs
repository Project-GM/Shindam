using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaPot : MonoBehaviour
{
    public GameObject waterImage;
    public int waterCount = 0;
    private void Update()
    {
        PourWater();
    }
    public void PourWater()
    {
        if (waterCount==3 && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("물이 가득찼습니다");
            return;
        }
        if (waterCount<3 && Input.GetKeyDown(KeyCode.Space))
        {
            waterCount++;
            waterImage.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 200 * waterCount);
        }
    }
}
