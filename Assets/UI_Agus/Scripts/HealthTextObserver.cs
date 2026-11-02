using UnityEngine;
using TMPro;


[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthTextObserver : MonoBehaviour
{
    private TextMeshProUGUI healthText;

    private void Awake()
    {
        healthText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        
        ShopHealth.OnHealthChanged += UpdateHealthText;
    }

    private void OnDisable()
    {
        
        ShopHealth.OnHealthChanged -= UpdateHealthText;
    }

    
    private void UpdateHealthText(float currentHealth, float maxHealth)
    {
        
        healthText.text = $"{Mathf.CeilToInt(currentHealth)} / {Mathf.CeilToInt(maxHealth)}";
    }
}
