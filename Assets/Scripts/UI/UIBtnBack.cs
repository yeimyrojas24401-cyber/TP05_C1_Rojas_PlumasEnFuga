using UnityEngine;
using UnityEngine.UI;

public class UIBtnBack : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject currentPanel;
    [SerializeField] private GameObject backPanel;
    [SerializeField] private Button backButton;
    private void Awake()
    {
        backButton = GetComponent<Button>();
        backButton.onClick.AddListener(OnBackClicked);
    }
    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
    }
    private void OnBackClicked()
    {
        currentPanel.SetActive(false);
        backPanel.SetActive(true);
    }
}
