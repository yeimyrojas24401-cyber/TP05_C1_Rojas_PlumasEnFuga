using TMPro;
using UnityEngine;

public class UISlowTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text slowText;
    [SerializeField] private GameObject slowTimerPanel;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private Animator animator;
    [SerializeField] private string animStateName = "SlowMoAnim";

    private float lastRemaining;

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
        bool show = remaining > 0f;
        slowTimerPanel.SetActive(show);

        if (show && remaining > lastRemaining)
        {
            animator.Play(animStateName, 0, 0f);
        }

        lastRemaining = remaining;
        slowText.text = remaining.ToString("0.0") + " s";
    }
    private void HideTimer()
    {
        slowTimerPanel.SetActive(false);
        lastRemaining = 0f;
    }
}
