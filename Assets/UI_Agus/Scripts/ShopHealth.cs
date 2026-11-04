using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class ShopHealth : MonoBehaviour
{
    public float maxHealth = 500f;
    private float currentHealth;

    
    public static event Action<float, float> OnHealthChanged;
    public static event Action OnDefeat;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    void Start()
    {
        currentHealth = maxHealth;
        NotifyHealthChange();
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return; 

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDefeat?.Invoke(); 
        }

        NotifyHealthChange();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        NotifyHealthChange();
    }

    public void IncreaseMaxHealth(float amount, bool healBonus = true)
    {
        maxHealth += amount;

        if (healBonus)
        {
            currentHealth += amount;
        }

        NotifyHealthChange();
    }

    private void NotifyHealthChange()
    {
        float pct = currentHealth / maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth); 
    }

    void Update()
    {
        
        if (Keyboard.current == null) return;

        
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            TakeDamage(50f);
            Debug.Log("Vida actual de la tienda: " + currentHealth);
        }

        
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            Heal(50f);
            Debug.Log("Vida actual de la tienda: " + currentHealth);
        }

        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            IncreaseMaxHealth(250f, true); 
            Debug.Log($"Nueva Vida Máxima: {maxHealth}");
        }
    }


}
