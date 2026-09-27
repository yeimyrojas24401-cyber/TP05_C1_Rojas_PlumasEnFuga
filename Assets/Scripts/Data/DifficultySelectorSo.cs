using UnityEngine;
[CreateAssetMenu(fileName = "DifficultySelectorSo", menuName = "Data/Game/DifficultySelectorData")]
public class DifficultySelectorSo : ScriptableObject
{
    [SerializeField] private DifficultySettingsSo currentDifficulty;
    public DifficultySettingsSo CurrentDifficulty => currentDifficulty;

    public void SetDifficulty(DifficultySettingsSo difficulty)
    {
        currentDifficulty = difficulty;
    }
}
