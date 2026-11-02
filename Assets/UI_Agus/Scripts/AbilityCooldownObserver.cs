using UnityEngine;
using UnityEngine.UI;
using TMPro; // Requerido para TextMeshPro[cite: 91]

public class AbilityCooldownObserver : MonoBehaviour
{
    [Header("Configuración de Habilidad")]
    [SerializeField] private AbilityManager.AbilityType targetAbility;

    [Header("Referencias UI")]
    [SerializeField] private Image cooldownOverlay; // La imagen "Filled" oscura
    [SerializeField] private TextMeshProUGUI cooldownText; // Texto del contador[cite: 91]

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
        // Solo reacciona si el evento corresponde a esta habilidad específica
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
            // Actualiza la sombra rellenándose/vaciándose (1.0 -> 0.0)
            if (cooldownOverlay != null)
            {
                cooldownOverlay.fillAmount = cooldownTimer / cooldownDuration;
            }

            // Muestra los segundos restantes con un decimal (ej: "3.4s")
            if (cooldownText != null)
            {
                cooldownText.text = cooldownTimer.ToString("F1") + "s";
            }
        }
    }
}