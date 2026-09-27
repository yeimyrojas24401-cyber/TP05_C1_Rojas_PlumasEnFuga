using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerDataSo data;

    public int CurrentLives { get; private set; }
    public int MaxLives => data.maxLives;
    public bool IsInvincible => invincibleTimer > 0f;

    private float invincibleTimer;
    private float powerUpTimer;

    public event UnityAction<int> OnLivesChanged;
    public event UnityAction<float> OnInvincibleTimeChanged;
    public event UnityAction OnDied;

    private void Awake()
    {
        CurrentLives = Mathf.Clamp(data.startingLives, 1, data.maxLives);
    }

    private void Update()
    {
        if (invincibleTimer > 0f)
            invincibleTimer -= Time.deltaTime;
        if (powerUpTimer > 0f)
        {
            powerUpTimer -= Time.deltaTime;
            OnInvincibleTimeChanged?.Invoke(Mathf.Max(powerUpTimer, 0f));
        }
    }

    public void AddLife(int amount = 1)
    {
        CurrentLives = Mathf.Min(CurrentLives + amount, data.maxLives);
        OnLivesChanged?.Invoke(CurrentLives);
    }

    public void SetInvincible(float duration)
    {
        invincibleTimer = Mathf.Max(invincibleTimer, duration);
        powerUpTimer = Mathf.Max(powerUpTimer, duration);
    }

    public void TakeHit(GameObject obstacle)
    {
        if (IsInvincible)
            return;

        CurrentLives--;
        OnLivesChanged?.Invoke(CurrentLives);

        if (CurrentLives <= 0)
        {
            OnDied?.Invoke();
            Destroy(gameObject);
            return;
        }

        Destroy(obstacle);
        invincibleTimer = data.invulnerabilityAfterHit;
    }
}
