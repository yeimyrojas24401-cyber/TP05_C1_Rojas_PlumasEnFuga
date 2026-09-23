using UnityEngine;

public class UILife : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject[] lifeIcons;

    private void Start()
    {
        playerHealth.OnLivesChanged += Refresh;
        Refresh(playerHealth.CurrentLives);
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnLivesChanged -= Refresh;
    }

    private void Refresh(int lives)
    {
        for (int i = 0; i < lifeIcons.Length; i++)
            lifeIcons[i].SetActive(i < lives);
    }
}
