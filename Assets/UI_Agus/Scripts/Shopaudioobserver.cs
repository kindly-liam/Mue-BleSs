using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShopAudioObserver : MonoBehaviour
{
    [Header("Clips de Audio de la Tienda")]
    [SerializeField] private AudioClip soundAbrirTienda;
    [SerializeField] private AudioClip soundCerrarTienda;
    [SerializeField] private AudioClip soundCompraExitosa;
    [SerializeField] private AudioClip soundCompraFallida;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        ShopManager.OnTiendaAbierta += ReproducirAbrir;
        ShopManager.OnTiendaCerrada += ReproducirCerrar;
        ShopManager.OnCompraExitosa += ReproducirExito;
        ShopManager.OnCompraFallida += ReproducirFallo;
    }

    private void OnDisable()
    {
        ShopManager.OnTiendaAbierta -= ReproducirAbrir;
        ShopManager.OnTiendaCerrada -= ReproducirCerrar;
        ShopManager.OnCompraExitosa -= ReproducirExito;
        ShopManager.OnCompraFallida -= ReproducirFallo;
    }

    private void ReproducirAbrir()
    {
        PlayClip(soundAbrirTienda);
    }

    private void ReproducirCerrar()
    {
        PlayClip(soundCerrarTienda);
    }

    private void ReproducirExito()
    {
        PlayClip(soundCompraExitosa);
    }

    private void ReproducirFallo()
    {
        PlayClip(soundCompraFallida);
    }

    private void PlayClip(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}