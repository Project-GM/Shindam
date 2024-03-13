using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTest : MonoBehaviour
{
    private InterationFloating interactionFloating;
    private GameObject player;
    private void Start()
    {
        interactionFloating = FindAnyObjectByType<InterationFloating>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (IsNearby())
        {
            interactionFloating.EnableFloatingImage(transform.position + new Vector3(0, 0.8f, 0));
            if (Input.GetKeyDown(KeyCode.E))
            {
                //제조 시작
            }
        }
        else
        {
            interactionFloating.DisableFloatingImage();
        }
    }
    public bool IsNearby()
    {
        return Vector2.Distance(transform.position, player.transform.position) < 1f;
    }
}
