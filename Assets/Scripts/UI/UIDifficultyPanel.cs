using UnityEngine;
using UnityEngine.UI;

public class UIDifficultyPanel : MonoBehaviour
{
    [SerializeField] private DifficultySelectorSo difficultySelector;
    [SerializeField] private DifficultySettingsSo easyDifficulty;
    [SerializeField] private DifficultySettingsSo hardDifficulty;
    [SerializeField] private Button btnEasy;
    [SerializeField] private Button btnHard;

    private void Awake()
    {
        btnEasy.onClick.AddListener(OnButtonEasyClicked);
        btnHard.onClick.AddListener(OnButtonHardClicked);
    }


    private void OnDestroy()
    {
        btnEasy.onClick.RemoveAllListeners();
        btnHard.onClick.RemoveAllListeners();
    }
    private void OnButtonEasyClicked()
    {
        difficultySelector.SetDifficulty(easyDifficulty);
    }

    private void OnButtonHardClicked()
    {
        difficultySelector.SetDifficulty(hardDifficulty);
    }
}
