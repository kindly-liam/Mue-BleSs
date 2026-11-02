using UnityEngine;
using System;
using UnityEngine.InputSystem; 

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    [Header("Configuración del Nivel")]
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int currentWave = 1;
    [SerializeField] private int maxWaves = 3;

   
    public static event Action<int, int> OnWaveChanged; // (oleadaActual, maxOleadas)
    public static event Action<int> OnLevelChanged;     // (nivelActual)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
        NotifyWaveChange();
        NotifyLevelChange();
    }

    private void Update()
    {
        if (Keyboard.current == null) return;

        // TECLA DE PRUEBA: N para pasar a la siguiente oleada
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            NextWave();
        }
    }

    public void NextWave()
    {
        if (currentWave < maxWaves)
        {
            currentWave++;
            NotifyWaveChange();
            Debug.Log($"Iniciando Oleada {currentWave}");
        }
        else
        {
            Debug.Log("¡Todas las oleadas completadas! Nivel superado.");
            
        }
    }

    private void NotifyWaveChange()
    {
        OnWaveChanged?.Invoke(currentWave, maxWaves);
    }

    private void NotifyLevelChange()
    {
        OnLevelChanged?.Invoke(currentLevel);
    }

    public void NextLevel()
    {
        currentLevel++;
        currentWave = 1; 

        NotifyLevelChange(); 
        NotifyWaveChange();  

        Debug.Log($"¡Avanzando al Nivel {currentLevel}!");
    }
}