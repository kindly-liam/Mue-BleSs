using UnityEngine;
using System;
using UnityEngine.InputSystem; 

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance { get; private set; }

    // Identificador para cada habilidad
    public enum AbilityType { Speed, Damage, FireRate }

    //(TipoHabilidad, CooldownTotal)
    public static event Action<AbilityType, float> OnAbilityUsed;

    [Header("Tiempos de Cooldown (Segundos)")]
    [SerializeField] private float speedCooldown = 5f;
    [SerializeField] private float damageCooldown = 8f;
    [SerializeField] private float fireRateCooldown = 4f;

    private float nextSpeedTime = 0f;
    private float nextDamageTime = 0f;
    private float nextFireRateTime = 0f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        
        if (Keyboard.current == null) return;

        // Habilidad C: Aumento de Velocidad
        if (Keyboard.current.cKey.wasPressedThisFrame && Time.time >= nextSpeedTime)
        {
            UseAbility(AbilityType.Speed, speedCooldown, ref nextSpeedTime);
        }

        
        if (Keyboard.current.qKey.wasPressedThisFrame && Time.time >= nextDamageTime)
        {
            UseAbility(AbilityType.Damage, damageCooldown, ref nextDamageTime);
        }

        
        if (Keyboard.current.fKey.wasPressedThisFrame && Time.time >= nextFireRateTime)
        {
            UseAbility(AbilityType.FireRate, fireRateCooldown, ref nextFireRateTime);
        }
    }

    private void UseAbility(AbilityType ability, float cooldown, ref float nextTimeVar)
    {
        nextTimeVar = Time.time + cooldown;

        
        OnAbilityUsed?.Invoke(ability, cooldown);

       
        Debug.Log($"Habilidad {ability} activada. Cooldown: {cooldown}s");
    }
}