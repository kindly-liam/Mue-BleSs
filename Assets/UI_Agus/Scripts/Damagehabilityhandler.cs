using UnityEngine;
using System.Collections;

public class DamageAbilityHandler : MonoBehaviour
{
    [Header("Configuración del Boost de Daño")]
    [Tooltip("Daño extra que se le suma a las balas mientras la habilidad está activa")]
    [SerializeField] private int danioAdicional = 2; 

    [Tooltip("Duración de la habilidad en segundos")]
    [SerializeField] private float duracionEfecto = 4f;

    public static int DanioActualExtra { get; private set; } = 0;

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
        if (ability == AbilityManager.AbilityType.Damage)
        {
            if (rutinaBoostActiva != null) StopCoroutine(rutinaBoostActiva);
            rutinaBoostActiva = StartCoroutine(RutinaBoostDanio());
        }
    }

    private IEnumerator RutinaBoostDanio()
    {
        DanioActualExtra = danioAdicional;
        Debug.Log($"¡Boost de daño activado! Daño extra: +{danioAdicional}");

        yield return new WaitForSeconds(duracionEfecto);

        DanioActualExtra = 0;
        Debug.Log("Boost de daño terminado.");
    }
}