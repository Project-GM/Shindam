using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaPot : MonoBehaviour
{
    public GameObject waterImage;
    public int waterCount = 0;
    public CanvasGroup waterFullFloating;
    private bool isFloatingMessage;
    private void OnEnable()
    {
        waterCount = 0;
        waterImage.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 200 * waterCount);
    }
    private void Update()
    {
        PourWater();
    }
    public void PourWater()
    {
        if (waterCount==3 && Input.GetKeyDown(KeyCode.Space))
        {
            if(!isFloatingMessage) StartCoroutine(EnableWaterFullFloating());
            return;
        }
        if (waterCount<3 && Input.GetKeyDown(KeyCode.Space))
        {
            waterCount++;
            waterImage.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 200 * waterCount);
        }
    }
    IEnumerator EnableWaterFullFloating()
    {
        isFloatingMessage = true;
        waterFullFloating.gameObject.SetActive(true);
        while (waterFullFloating.alpha < 1)
        {
            waterFullFloating.alpha += Time.deltaTime * 1.5f;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        while (waterFullFloating.alpha > 0)
        {
            waterFullFloating.alpha -= Time.deltaTime * 1.5f;
            yield return null;
        }
        waterFullFloating.gameObject.SetActive(false);
        isFloatingMessage = false;
    }
}
