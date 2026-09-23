using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingLives = 1;
    [SerializeField] private int maxLives = 3;
    [SerializeField] private float invulnerabilityAfterHit = 1.5f;
}
