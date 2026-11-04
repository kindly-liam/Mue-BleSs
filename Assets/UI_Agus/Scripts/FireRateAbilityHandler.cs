using UnityEngine;
using System.Collections;

public class FireRateAbilityHandler : MonoBehaviour
{
    [Header("Configuración del Boost de Cadencia")]
    [Tooltip("Multiplicador de velocidad (Ej: 2 = el tiempo entre disparos se reduce a la mitad)")]
    [SerializeField] private float multiplicadorCadencia = 2f;

    [Tooltip("Duración de la habilidad en segundos")]
    [SerializeField] private float duracionEfecto = 5f;

   
    public static float MultiplicadorActual { get; private set; } = 1f;

    private Coroutine rutinaBoostActiva;

    private void OnEnable()
    {
        AbilityManager.OnAbilityUsed += HandleAbilityUsed;
    }

    private void OnDisable()
    {
        AbilityManager.OnAbilityUsed -= HandleAbilityUsed;
    }

    private void HandleAbilityUsed(AbilityManager.AbilityType ability, float cooldown)
    {
        if (ability == AbilityManager.AbilityType.FireRate)
        {
            if (rutinaBoostActiva != null) StopCoroutine(rutinaBoostActiva);
            rutinaBoostActiva = StartCoroutine(RutinaBoostCadencia());
        }
    }

    private IEnumerator RutinaBoostCadencia()
    {
        MultiplicadorActual = multiplicadorCadencia;
        Debug.Log($"¡Boost de cadencia activado! Disparando x{multiplicadorCadencia} más rápido.");

        yield return new WaitForSeconds(duracionEfecto);

        MultiplicadorActual = 1f;
        Debug.Log("Boost de cadencia terminado.");
    }
}