using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [Header("Configuración del Raycast")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float distanciaInteraccion = 6f;

    [Header("Referencias de Managers")]
    [SerializeField] private ShopManager shopManager;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Interact.performed += ctx => IntentarInteraccion();
    }

    private void OnEnable() => inputActions.Player.Enable();
    private void OnDisable() => inputActions.Player.Disable();

    private void IntentarInteraccion()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * distanciaInteraccion, Color.red, 2f);

        if (Physics.Raycast(ray, out hit, distanciaInteraccion))
        {
            if (hit.collider.CompareTag("Tienda"))
            {
                AbrirMenuTienda();
            }
        }
    }

    private void AbrirMenuTienda()
    {
        
        if (shopManager == null)
        {
            shopManager = FindFirstObjectByType<ShopManager>();
        }

        if (shopManager != null)
        {
            
            shopManager.AbrirTienda();
        }
    }
}