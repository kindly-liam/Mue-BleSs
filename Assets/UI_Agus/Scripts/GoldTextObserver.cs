using UnityEngine;
using TMPro; 

[RequireComponent(typeof(TextMeshProUGUI))]
public class GoldTextObserver : MonoBehaviour
{
    private TextMeshProUGUI goldText;

    private void Awake()
    {
        goldText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GoldManager.OnGoldChanged += UpdateGoldText;
    }

    private void OnDisable()
    {
        GoldManager.OnGoldChanged -= UpdateGoldText;
    }

    private void UpdateGoldText(int amount)
    {
        goldText.text = $"Gold: {amount}";
    }
}
