using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuObserver : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private GameObject hudGameplay; 

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        HideMenu();
    }

    private void OnEnable()
    {
        PauseManager.OnPauseToggle += HandlePauseToggle;
    }

    private void OnDisable()
    {
        PauseManager.OnPauseToggle -= HandlePauseToggle;
    }

    private void HandlePauseToggle(bool isPaused)
    {
        if (isPaused)
        {
            ShowMenu();
        }
        else
        {
            HideMenu();
        }
    }

    private void ShowMenu()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        if (hudGameplay != null)
        {
            hudGameplay.SetActive(false);
        }
    }

    private void HideMenu()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        if (hudGameplay != null)
        {
            hudGameplay.SetActive(true); 
        }
    }

    

    public void BotonContinuar()
    {
        if (PauseManager.Instance != null)
        {
            PauseManager.Instance.ResumeGame();
        }
    }

    public void BotonVolverAlMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenuLiamEna"); 
    }

    public void BotonSalir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}