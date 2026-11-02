using UnityEngine;
using TMPro;

public class WaveTextObserver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoRonda;

    private void OnEnable()
    {
        
        WaveManager.OnWaveChanged += ActualizarTexto;
    }

    private void OnDisable()
    {
        WaveManager.OnWaveChanged -= ActualizarTexto;
    }

    private void ActualizarTexto(int waveEnNivel)
    {
        if (textoRonda != null)
        {
            textoRonda.text = $"Wave: {waveEnNivel}/3";
        }
    }
}
