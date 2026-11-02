using UnityEngine;
using System;
using UnityEngine.InputSystem; 

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    public static bool IsPaused { get; private set; } = false;

    
    public static event Action<bool> OnPauseToggle;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (Keyboard.current == null) return;

        // Presionar la tecla ESCAPE para alternar la pausa
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;

        
        Time.timeScale = IsPaused ? 0f : 1f;

        
        Cursor.lockState = IsPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = IsPaused;

        
        OnPauseToggle?.Invoke(IsPaused);
    }

    public void ResumeGame()
    {
        if (IsPaused)
        {
            TogglePause();
        }
    }
}