using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterationFloating : MonoBehaviour
{
    private GameObject floatingImage;
    private void Start()
    {
        floatingImage = transform.GetChild(0).gameObject;
    }
    public void EnableFloatingImage(Vector3 position)
    {
        floatingImage.SetActive(true);
        floatingImage.transform.position = Camera.main.WorldToScreenPoint(position);
    }
    public void DisableFloatingImage()
    {
        floatingImage.SetActive(false);
    }
}
