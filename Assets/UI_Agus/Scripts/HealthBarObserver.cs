using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthBarObserver : MonoBehaviour
{
    [Header("Configuración de Colores")]
    
    [SerializeField] private Gradient healthGradient;

    private Image healthBarImage;

    private void Awake()
    {
        healthBarImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        
        ShopHealth.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        
        ShopHealth.OnHealthChanged -= UpdateHealthBar;
    }

    
    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        
        float healthPercentage = currentHealth / maxHealth;

        healthBarImage.fillAmount = healthPercentage; 
        healthBarImage.color = healthGradient.Evaluate(healthPercentage);
    }
}