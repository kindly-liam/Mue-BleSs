using UnityEngine;
using UnityEngine.SceneManagement; 

public class VictoryManager : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private GameObject victoryPanel;

    [Header("Configuración")]
    [Tooltip("El nombre exacto de la escena de tu menú principal")]
    [SerializeField] private string nombreEscenaMenu = "MainMenuLiamEna";

    private void OnEnable()
    {
        EnemyBase.OnBossDefeated += MostrarPantallaVictoria;
    }

    private void OnDisable()
    {
        EnemyBase.OnBossDefeated -= MostrarPantallaVictoria;
    }

    private void MostrarPantallaVictoria()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);

            
            Time.timeScale = 0f;

            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    

    public void VolverAlMenu()
    {
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreEscenaMenu);
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); 
    }
}