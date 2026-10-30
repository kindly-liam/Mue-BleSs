using UnityEngine;
using TMPro; 

[RequireComponent(typeof(TextMeshProUGUI))]
public class WaveTextObserver : MonoBehaviour
{
    private TextMeshProUGUI waveText;

    private void Awake()
    {
        waveText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        WaveManager.OnWaveChanged += UpdateWaveText;
    }

    private void OnDisable()
    {
        WaveManager.OnWaveChanged -= UpdateWaveText;
    }

    private void UpdateWaveText(int currentWave, int maxWaves)
    {
        
        waveText.text = $"Oleada: {currentWave} / {maxWaves}";
    }
}
