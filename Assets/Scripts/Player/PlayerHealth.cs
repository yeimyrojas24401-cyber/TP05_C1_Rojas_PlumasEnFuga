using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingLives = 1;
    [SerializeField] private int maxLives = 3;
    [SerializeField] private float invulnerabilityAfterHit = 1.5f;

    public int CurrentLives { get; private set; }
    public int MaxLives => maxLives;
    public bool IsInvincible => invincibleTimer > 0f;

    private float invincibleTimer;

    public UnityAction<int> OnLivesChanged;

    private void Awake()
    {
        CurrentLives = Mathf.Clamp(startingLives, 1, maxLives);
    }

    private void Update()
    {
        if (invincibleTimer > 0f)
            invincibleTimer -= Time.deltaTime;
    }

    public void AddLife(int amount = 1)
    {
        CurrentLives = Mathf.Min(CurrentLives + amount, maxLives);
        OnLivesChanged?.Invoke(CurrentLives);
    }

    public void SetInvincible(float duration)
    {
        invincibleTimer = Mathf.Max(invincibleTimer, duration);
    }

    public void TakeHit(GameObject obstacle)
    {
        if (IsInvincible)
            return;

        CurrentLives--;
        OnLivesChanged?.Invoke(CurrentLives);

        if (CurrentLives <= 0)
        {
            Object.FindFirstObjectByType<GameManager>().GameOver();
            Destroy(gameObject);
            return;
        }

        Destroy(obstacle);
        invincibleTimer = invulnerabilityAfterHit;
    }
}
