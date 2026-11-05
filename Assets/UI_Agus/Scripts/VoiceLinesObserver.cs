using JetBrains.Annotations;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VoiceLinesObserver : MonoBehaviour
{
    [Header("Dialogos")]
    [SerializeField] private AudioClip soundEmpiezaRonda;
    [SerializeField] private AudioClip soundEmpiezaRondaJohn;
    [SerializeField] private AudioClip soundEmpiezaRondaJohn2;
    [SerializeField] private AudioClip soundPerder;
    [SerializeField] private AudioClip soundPerderJohn;
    [SerializeField] private AudioClip soundPerderJohn2;
    [SerializeField] private AudioClip soundBossFight;
    [SerializeField] private AudioClip soundBossFightJohn;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        string nombreEscena = SceneManager.GetActiveScene().name;
        audioSource = GetComponent<AudioSource>();
        if (nombreEscena == "Boss Fight")
        {
            StartCoroutine(BossFight());
        }

    }

    public void ReproducirSecuenciaInicio()
    {
        Debug.Log("ReproducirSecuenciaInicio llamada.");
        StartCoroutine(SecuenciaDeAudios());
    }

    public IEnumerator SecuenciaDeAudios()
    {
        Debug.Log("Iniciando SecuenciaDeAudios. Reproduciendo: " + (soundEmpiezaRonda != null ? soundEmpiezaRonda.name : "NULL"));

        if (soundEmpiezaRonda != null)
        {
            audioSource.PlayOneShot(soundEmpiezaRonda);
        }

        yield return new WaitForSeconds(2f);

        AudioClip clipAleatorio;
        int randomIndex = Random.Range(0, 2);
        Debug.Log("Random Index elegido (Inicio): " + randomIndex);

        if (randomIndex == 0)
        {
            clipAleatorio = soundEmpiezaRondaJohn;
        }
        else
        {
            clipAleatorio = soundEmpiezaRondaJohn2;
        }

        Debug.Log("Clip aleatorio seleccionado (Inicio): " + (clipAleatorio != null ? clipAleatorio.name : "NULL"));

        if (clipAleatorio != null)
        {
            audioSource.PlayOneShot(clipAleatorio);
        }
    }

    public void ReproducirSecuenciaDerrota()
    {
        Debug.Log("ReproducirSecuenciaDerrota llamada.");
        StartCoroutine(SecuenciaDeDerrotaCorrutina());
    }

    private IEnumerator SecuenciaDeDerrotaCorrutina()
    {
        Debug.Log("Iniciando SecuenciaDeDerrotaCorrutina. Reproduciendo: " + (soundPerder != null ? soundPerder.name : "NULL"));

        if (soundPerder != null)
        {
            audioSource.PlayOneShot(soundPerder);
        }

        yield return new WaitForSeconds(2f);

        audioSource.PlayOneShot(soundPerderJohn);

    }
    public IEnumerator BossFight()
    {
        audioSource.PlayOneShot(soundBossFight);


        yield return new WaitForSeconds(2f);


        audioSource.PlayOneShot(soundBossFightJohn);

    }
}

