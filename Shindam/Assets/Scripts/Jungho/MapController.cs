using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public WorldMapUI worldMapUI;
    // Start is called before the first frame update
    void Start()
    {
        hide();
    }
    // Update is called once per frame
    public void show()
    {
        worldMapUI.gameObject.SetActive(true);
    }
    public void hide()
    {
        worldMapUI.gameObject.SetActive(false);
    }
}
