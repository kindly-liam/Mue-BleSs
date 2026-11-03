using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class AbilityCooldownObserver : MonoBehaviour
{
    [Header("Configuración de Habilidad")]
    [SerializeField] private AbilityManager.AbilityType targetAbility;

    [Header("Referencias UI")]
    [SerializeField] private Image cooldownOverlay; 
    [SerializeField] private TextMeshProUGUI cooldownText; 
    private bool isCoolingDown = false;
    private float cooldownDuration;
    private float cooldownTimer;

    private void Start()
    {
        if (cooldownOverlay != null) cooldownOverlay.fillAmount = 0f;
        if (cooldownText != null) cooldownText.text = "";
    }

    private void OnEnable()
    {
        AbilityManager.OnAbilityUsed += HandleAbilityUsed;
    }

    private void OnDisable()
    {
        AbilityManager.OnAbilityUsed -= HandleAbilityUsed;
    }

    private void HandleAbilityUsed(AbilityManager.AbilityType ability, float duration)
    {
       
        if (ability == targetAbility)
        {
            cooldownDuration = duration;
            cooldownTimer = duration;
            isCoolingDown = true;
        }
    }

    private void Update()
    {
        if (!isCoolingDown) return;

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            isCoolingDown = false;
            if (cooldownOverlay != null) cooldownOverlay.fillAmount = 0f;
            if (cooldownText != null) cooldownText.text = "";
        }
        else
        {
            
            if (cooldownOverlay != null)
            {
                cooldownOverlay.fillAmount = cooldownTimer / cooldownDuration;
            }

            
            if (cooldownText != null)
            {
                cooldownText.text = cooldownTimer.ToString("F1") + "s";
            }
        }
    }
}