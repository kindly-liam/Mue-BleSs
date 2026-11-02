using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class DefeatScreenObserver : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private GameObject hudGameplay;

    [Header("Configuración de Animación")]
    [SerializeField] private float fadeDuration = 1.5f;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void OnEnable()
    {
        ShopHealth.OnDefeat += HandleDefeat;
    }

    private void OnDisable()
    {
        ShopHealth.OnDefeat -= HandleDefeat;
    }

    private void HandleDefeat()
    {
        if (hudGameplay != null)
        {
            hudGameplay.SetActive(false);
        }

        StartCoroutine(FadeInScreen());
    }

    private IEnumerator FadeInScreen()
    {
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        float elapsedTime = 0f;

        
        while (elapsedTime < fadeDuration)
        {
           
            elapsedTime += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        
        Time.timeScale = 0f;
    }

    

    public void VolverAlMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenuLiamEna"); 
    }

    public void SalirJuego()
    {
        Debug.Log("Cerrando el juego...");
        Application.Quit();

        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
