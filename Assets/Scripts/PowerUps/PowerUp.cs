using UnityEngine;

enum PowerUpType
{
    None = 0,
    Life = 1,
    Invencibility = 2,
    Slow = 3,
    Last
}
public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUpType = PowerUpType.None;
    [SerializeField] private float duration = 25f;
    [SerializeField] private AudioClip pickupClip;

    public AudioClip PickupClip => pickupClip;
    public void DoAction(GameObject player)
    {
        Debug.Log($"DoAction ejecutado, tipo: {powerUpType}");

        switch (powerUpType) //nota nunca un powerUpsera none ni last ni default sin embargo lo dejamos
        {
            case PowerUpType.None:
                break;
            case PowerUpType.Life:
                player.GetComponent<PlayerHealth>().AddLife();
                break;
            case PowerUpType.Invencibility:
                player.GetComponent<PlayerHealth>().SetInvincible(duration);
                break;
            case PowerUpType.Slow:
                Object.FindFirstObjectByType<ObstacleSpawner>().TriggerSlowEffect(duration);
                break;
            case PowerUpType.Last:
                break;
            default:
                break;
        }

    }
}
