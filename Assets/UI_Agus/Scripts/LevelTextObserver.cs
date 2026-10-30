using UnityEngine;
using TMPro; 

[RequireComponent(typeof(TextMeshProUGUI))]
public class LevelTextObserver : MonoBehaviour
{
    private TextMeshProUGUI levelText;

    private void Awake()
    {
        levelText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        WaveManager.OnLevelChanged += UpdateLevelText;
    }

    private void OnDisable()
    {
        WaveManager.OnLevelChanged -= UpdateLevelText;
    }

    private void UpdateLevelText(int currentLevel)
    {
        
        levelText.text = $"Nivel {currentLevel}";
    }
}
