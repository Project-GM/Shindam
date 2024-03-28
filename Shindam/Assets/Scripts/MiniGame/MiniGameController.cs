using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    [SerializeField]
    private TimingMiniGame timingMiniGame;
    [SerializeField]
    private ButtonMashingMiniGame buttonMashingMiniGame;

    public event Action<bool> OnEndTimingMiniGame;
    public event Action<int> OnEndButtonMashingMiniGame;

    private void Awake()
    {
        buttonMashingMiniGame.OnEndMiniGame += EndButtonMashingMiniGame;
    }
    public void StartTimingMiniGame()
    {

    }
    public void EndTimingMiniGame()
    {

    }
    public void StartButtonMashingMiniGame(Vector3 targetPosition)
    {
        Debug.Log("미니게임 매니저");
        PlayerAction.s_Instance.isInteracting = true;
        buttonMashingMiniGame.StartMiniGame(targetPosition);
    }
    public void EndButtonMashingMiniGame(int mashCount)
    {
        PlayerAction.s_Instance.isInteracting = false;
        int returnIndex = 0;
        if (mashCount == 0) returnIndex = 0;
        else if (mashCount > 0 && mashCount <= 10) returnIndex = 1;
        else if (mashCount > 10 && mashCount <= 24) returnIndex = 2;
        else returnIndex = 3;
        OnEndButtonMashingMiniGame?.Invoke(returnIndex);
    }
}
