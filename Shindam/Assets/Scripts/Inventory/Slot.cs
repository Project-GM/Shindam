using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TMP_Text itemCountText;
    private TestItem _item;
    public TestItem item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.itemImage;
                image.color = new Color(1, 1, 1, 1);
                itemCountText.gameObject.SetActive(true);
                itemCountText.text = item.itemCount.ToString();
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
                itemCountText.gameObject.SetActive(false);
            }
        }
    }
}
