using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UISlowTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text slowText;
    [SerializeField] private GameObject slowTimerPanel;
    [SerializeField] private ObstacleSpawner obstacleSpawner;

    private void Start()
    {
        slowTimerPanel.SetActive(false);
        obstacleSpawner.OnSlowTimeChanged += UpdateTimer;
        obstacleSpawner.OnSlowEffectEnded += HideTimer;
    }


    private void OnDestroy()
    {
        if (obstacleSpawner != null)
        {
            obstacleSpawner.OnSlowTimeChanged -= UpdateTimer;
            obstacleSpawner.OnSlowEffectEnded -= HideTimer;
        }
    }

    private void UpdateTimer(float remaining)
    {
        slowTimerPanel.SetActive(true);
        slowText.text = remaining.ToString("0.0") + " s";
    }

    private void HideTimer()
    {
        slowTimerPanel.SetActive(false);
    }
}
