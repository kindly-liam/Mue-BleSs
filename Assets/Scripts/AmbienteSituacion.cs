using System.Collections;
using UnityEngine;

public class AmbienteSituacion : MonoBehaviour
{
    [Header("Fuentes")]
    [SerializeField] AudioSource ambienteTienda;   
    [SerializeField] AudioSource ambienteInfernal; 

    [Header("Volúmenes")]
    [SerializeField, Range(0f, 1f)] float tiendaCalma = 1f;    
    [SerializeField, Range(0f, 1f)] float tiendaOleada = 0f;  
    [SerializeField, Range(0f, 1f)] float infernalCalma = 0f;  
    [SerializeField, Range(0f, 1f)] float infernalOleada = 1f; 

    [Header("Transiciones (segundos)")]
    [SerializeField] float fadeEntradaOleada = 1.5f;
    [SerializeField] float fadeSalidaOleada = 4f;

    Coroutine fadeActual;

    void Start()
    {
        
        ambienteTienda.volume = tiendaCalma;
        ambienteInfernal.volume = infernalCalma;
    }

    private void OnEnable()
    {
       
        WaveManager.OnWaveChanged += ManejarInicioOleada;
        Spawner.OnWaveCompleted += TerminarOleada;
    }

    private void OnDisable()
    {
        
        WaveManager.OnWaveChanged -= ManejarInicioOleada;
        Spawner.OnWaveCompleted -= TerminarOleada;
    }

   
    private void ManejarInicioOleada(int numeroOleada)
    {
        IniciarOleada();
    }

    [ContextMenu("Probar: iniciar oleada")]
    public void IniciarOleada() => Fade(tiendaOleada, infernalOleada, fadeEntradaOleada);

    [ContextMenu("Probar: terminar oleada")]
    public void TerminarOleada() => Fade(tiendaCalma, infernalCalma, fadeSalidaOleada);

    void Fade(float volTienda, float volInfernal, float duracion)
    {
        if (fadeActual != null) StopCoroutine(fadeActual);
        fadeActual = StartCoroutine(FadeRutina(volTienda, volInfernal, duracion));
    }

    IEnumerator FadeRutina(float volTienda, float volInfernal, float duracion)
    {
        float t = 0f;
        float inicioTienda = ambienteTienda.volume;
        float inicioInfernal = ambienteInfernal.volume;

        while (t < duracion)
        {
            t += Time.deltaTime;
            float k = t / duracion;
            ambienteTienda.volume = Mathf.Lerp(inicioTienda, volTienda, k);
            ambienteInfernal.volume = Mathf.Lerp(inicioInfernal, volInfernal, k);
            yield return null;
        }

        ambienteTienda.volume = volTienda;
        ambienteInfernal.volume = volInfernal;
    }
}