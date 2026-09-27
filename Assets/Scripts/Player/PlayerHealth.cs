using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerDataSo data;

    public int CurrentLives { get; private set; }
    public int MaxLives => data.maxLives;
    public bool IsInvincible => invincibleTimer > 0f;

    private float invincibleTimer;

    public event UnityAction<int> OnLivesChanged;
    public event UnityAction OnDied;

    private void Awake()
    {
        CurrentLives = Mathf.Clamp(data.startingLives, 1, data.maxLives);
    }

    private void Update()
    {
        if (invincibleTimer > 0f)
            invincibleTimer -= Time.deltaTime;
    }

    public void AddLife(int amount = 1)
    {
        CurrentLives = Mathf.Min(CurrentLives + amount, data.maxLives);
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
            OnDied?.Invoke();
            Destroy(gameObject);
            return;
        }

        Destroy(obstacle);
        invincibleTimer = data.invulnerabilityAfterHit;
    }
}
