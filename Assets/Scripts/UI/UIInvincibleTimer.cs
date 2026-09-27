using UnityEngine;
using TMPro;
using System;

public class UIInvincibleTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject invinciblePanel;

    private void Start()
    {
        invinciblePanel.SetActive(false);
        playerHealth.OnInvincibleTimeChanged += UpdateTimer;
    }
    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnInvincibleTimeChanged -= UpdateTimer;
    }

    private void UpdateTimer(float remaining)
    {
        invinciblePanel.SetActive(remaining > 0);
        timerText.text = remaining.ToString("0.0") + " s";
    }
}
