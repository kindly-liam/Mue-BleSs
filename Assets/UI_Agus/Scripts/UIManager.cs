using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private Slider shopHealthSlider;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI waveText;

    private int currentPoints = 0;
    private float maxShopHealth = 100f;
    private float currentShopHealth;

    private void Start()
    {
        currentShopHealth = maxShopHealth;
        UpdateUI();
    }

    public void ModifyGold(int amount)
    {
        currentPoints += amount;
        UpdateUI();
    }

    public void DamageShop(float damage)
    {
        currentShopHealth = Mathf.Clamp(currentShopHealth - damage, 0, maxShopHealth);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (shopHealthSlider) shopHealthSlider.value = currentShopHealth / maxShopHealth;
        if (goldText) goldText.text = $"puntos: {currentPoints}";
        if (waveText) waveText.text = "Oleada: 1/3";
    }
}