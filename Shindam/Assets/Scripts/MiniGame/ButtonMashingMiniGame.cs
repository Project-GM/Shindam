using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMashingMiniGame : MonoBehaviour
{
    [SerializeField]
    private ProgressBar progressBarUI;
    [SerializeField]
    private TimerUI timerUI;

    private Vector3 targetPosition = Vector3.zero;

    public event Action<int> OnEndMiniGame;

    private void Awake()
    {
        timerUI.OnEndTimer += EndMiniGame;
        Hide();
    }
    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(targetPosition + new Vector3(0, -0.5f, 0));
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void StartMiniGame(Vector3 targetPosition)
    {
        Debug.Log("연타 미니게임 스크립트");
        Show();
        this.targetPosition = targetPosition;
        progressBarUI.Reset();
        timerUI.Reset();
        timerUI.StartTimer();
    }
    public void EndMiniGame()
    {
        OnEndMiniGame?.Invoke(progressBarUI.GetMashCount());
        Hide();
    }
}
