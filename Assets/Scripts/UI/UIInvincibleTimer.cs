using TMPro;
using UnityEngine;

public class UIInvincibleTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject invinciblePanel;
    [SerializeField] private Animator animator;
    [SerializeField] private string animStateName = "InvencibleAnim";

    private float lastRemaining;

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
        bool show = remaining > 0f;
        invinciblePanel.SetActive(show);

        if (show && remaining > lastRemaining)
        {
            animator.Play(animStateName, 0, 0f);
        }

        lastRemaining = remaining;
        timerText.text = remaining.ToString("0.0") + " s";
    }
}
