using UnityEngine;
using System;
using UnityEngine.InputSystem; 

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance { get; private set; }

    [SerializeField] private int currentGold = 0;

    
    public static event Action<int> OnGoldChanged;

    public int CurrentGold => currentGold;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        NotifyGoldChange();
    }

    private void Update()
    {
        if (Keyboard.current == null) return;

        
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            AddGold(50);
        }
    }

    // Método que llaman los diablillos cuando mueran
    public void AddGold(int amount)
    {
        currentGold += amount;
        NotifyGoldChange();
    }

    
    public bool TrySpendGold(int cost)
    {
        if (currentGold >= cost)
        {
            currentGold -= cost;
            NotifyGoldChange();
            return true;
        }

        Debug.Log("No tenés suficiente oro!");
        return false;
    }

    private void NotifyGoldChange()
    {
        OnGoldChanged?.Invoke(currentGold);
    }
}
