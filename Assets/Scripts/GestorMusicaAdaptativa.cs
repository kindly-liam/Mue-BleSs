
using UnityEngine;
using UnityEngine.Audio;

public class GestorMusicaAdaptativa : MonoBehaviour
{
    public static GestorMusicaAdaptativa Instancia;

    [Header("Snapshots del Mixer")]
    public AudioMixerSnapshot snapshotEntreOleadas;
    
    public AudioMixerSnapshot snapshotOleadaNormal;
    public AudioMixerSnapshot snapshotPeleaBoss;

    [Header("Ajustes de Tiempo")]
    public float tiempoTransicionCalma = 2.5f;
    public float tiempoTransicionAccion = 1.0f;

    void Awake()
    {
        // Sistema para evitar que el objeto se duplique al recargar escenas
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject); // Evita que se destruya al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Si ya existía uno de antes, destruye el nuevo duplicado
        }
    }

    public void ActivarEntreOleadas()
    {
        snapshotEntreOleadas.TransitionTo(tiempoTransicionCalma);
    }

 

    public void ActivarOleadaNormal()
    {
        snapshotOleadaNormal.TransitionTo(tiempoTransicionAccion);
    }

    public void ActivarPeleaBoss()
    {
        snapshotPeleaBoss.TransitionTo(tiempoTransicionAccion);
    }
}