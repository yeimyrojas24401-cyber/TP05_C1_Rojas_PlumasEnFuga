using UnityEngine;

public class BiomeSelector : MonoBehaviour
{
    [SerializeField] private DifficultySelectorSo difficultySelector;

    [SerializeField] private GameObject[] biomes;
    
    private void Awake()
    {
        int biomeIndex = difficultySelector.CurrentDifficulty.BiomeIndex;

        for(int i = 0; i < biomes.Length; i++)
            biomes[i].SetActive(i == biomeIndex);
    }
}
