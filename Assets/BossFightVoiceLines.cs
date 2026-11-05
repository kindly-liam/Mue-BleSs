using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BossSceneAudio : MonoBehaviour
{
    [SerializeField] private AudioClip BossFight;
    [SerializeField] private AudioClip BossFightJohn;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string nombreEscenaBoss = "BossFight";

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (SceneManager.GetActiveScene().name == nombreEscenaBoss)
        {
            StartCoroutine(ReproducirSecuenciaBoss());
        }
    }

    private IEnumerator ReproducirSecuenciaBoss()
    {
        if (BossFight != null)
        {
            audioSource.PlayOneShot(BossFight);
        }

        yield return new WaitForSeconds(2f);

        if (BossFightJohn!= null)
        {
            audioSource.PlayOneShot(BossFightJohn);
        }
    }
}