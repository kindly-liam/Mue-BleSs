using UnityEngine;
using System.Collections;
using StarterAssets; 

[RequireComponent(typeof(ThirdPersonController))]
public class SpeedAbilityHandler : MonoBehaviour
{
    [Header("Configuración del Boost de Velocidad")]
    [Tooltip("Por cuánto se multiplica la velocidad (Ej: 1.5 = 50% más rápido)")]
    [SerializeField] private float multiplicadorVelocidad = 1.5f;

    [Tooltip("Cuánto dura el efecto de velocidad en segundos")]
    [SerializeField] private float duracionEfecto = 3f;

    private ThirdPersonController playerController;
    private float velocidadBaseMove;
    private float velocidadBaseSprint;
    private Coroutine rutinaBoostActiva;

    private void Awake()
    {
        
        playerController = GetComponent<ThirdPersonController>();
    }

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
        
        if (ability == AbilityManager.AbilityType.Speed)
        {
            if (rutinaBoostActiva != null)
            {
                StopCoroutine(rutinaBoostActiva);
            }
            rutinaBoostActiva = StartCoroutine(RutinaBoostVelocidad());
        }
    }

    private IEnumerator RutinaBoostVelocidad()
    {
        
        velocidadBaseMove = playerController.MoveSpeed;
        velocidadBaseSprint = playerController.SprintSpeed;

        
        playerController.MoveSpeed *= multiplicadorVelocidad;
        playerController.SprintSpeed *= multiplicadorVelocidad;

        Debug.Log("¡Boost de velocidad activado!");

        
        yield return new WaitForSeconds(duracionEfecto);

        
        playerController.MoveSpeed = velocidadBaseMove;
        playerController.SprintSpeed = velocidadBaseSprint;

        Debug.Log("Boost de velocidad terminado.");
    }
}