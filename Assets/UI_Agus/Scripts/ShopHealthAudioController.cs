using UnityEngine;
using UnityEngine.Audio;

public class ShopHealthAudioController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private ShopHealth shopHealth;

    [Header("Parámetros Expuestos del Mixer")]
    [SerializeField] private string paramPitch = "MusicPitch";
    [SerializeField] private string paramCutoff = "LowpassCutoff";

    private void Update()
    {
        if (shopHealth == null || audioMixer == null) return;

        
        float porcentajeVida = shopHealth.CurrentHealth / shopHealth.MaxHealth;

       
        float pitchCalculado = Mathf.Lerp(1.15f, 0.9f, porcentajeVida);
        audioMixer.SetFloat(paramPitch, pitchCalculado);

        
        float cutoffCalculado = (porcentajeVida < 0.3f) ? 1200f : 22000f;
        audioMixer.SetFloat(paramCutoff, cutoffCalculado);
    }
}