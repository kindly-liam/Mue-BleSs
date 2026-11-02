using UnityEngine;
using System;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    [Header("Referencias")]
    [SerializeField] private Spawner spawner;

    [Header("Configuración de Oleadas")]
    [SerializeField] private int enemigosBase = 5;
    [SerializeField] private int incrementoPorOleada = 2;
    [SerializeField] private int maximoEnemigosPorOleada = 25;
    [SerializeField] private float tiempoEntreOleadas = 5f;

    private int oleadaAbsoluta = 0; // Cuenta total interna (1, 2, 3, 4, 5...)
    public int NivelActual { get; private set; } = 0;

    // Eventos separados para UI
    public static event Action<int> OnWaveChanged;
    public static event Action<int> OnLevelChanged;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        IniciarSiguienteOleada();
    }

    private void OnEnable()
    {
        Spawner.OnWaveCompleted += HandleWaveCompleted;
    }

    private void OnDisable()
    {
        Spawner.OnWaveCompleted -= HandleWaveCompleted;
    }

    private void HandleWaveCompleted()
    {
        StartCoroutine(RutinaTiempoEntreOleadas());
    }

    private IEnumerator RutinaTiempoEntreOleadas()
    {
        yield return new WaitForSeconds(tiempoEntreOleadas);
        IniciarSiguienteOleada();
    }

    public void IniciarSiguienteOleada()
    {
        oleadaAbsoluta++;

        
        int nuevoNivel = ((oleadaAbsoluta - 1) / 3) + 1;

        
        int waveEnNivel = ((oleadaAbsoluta - 1) % 3) + 1;

        
        OnWaveChanged?.Invoke(waveEnNivel);

        
        if (nuevoNivel != NivelActual)
        {
            NivelActual = nuevoNivel;
            OnLevelChanged?.Invoke(NivelActual);
        }

        
        int cantidadCalculada = enemigosBase + ((oleadaAbsoluta - 1) * incrementoPorOleada);
        int enemigosAInstanciar = Mathf.Min(cantidadCalculada, maximoEnemigosPorOleada);

        if (spawner != null)
        {
            spawner.IniciarOleada(enemigosAInstanciar);
        }
    }
}