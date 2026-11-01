using UnityEngine;
using System;
using UnityEngine.InputSystem; 

public class InteractionController : MonoBehaviour
{
    [Header("Configuración de Distancia")]
    [SerializeField] private float distanciaCamara = 3f; 
    [SerializeField] private float alcanceJugador = 3f;  

    
    public static event Action<bool> OnShopHover;

    private bool isHovering = false;

    
    [SerializeField] private ShopManager shopManager;

    void Update()
    {
        
        float rayDistance = distanciaCamara + alcanceJugador;

       
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, rayDistance))
        {
            
            if (hit.collider.CompareTag("Tienda"))
            {
                if (!isHovering)
                {
                    isHovering = true;
                    OnShopHover?.Invoke(true); 
                }

                
                if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
                {
                    if (shopManager != null)
                    {
                        shopManager.AbrirTienda(); 

                        
                        PerderFoco();
                    }
                }
            }
            else
            {
                PerderFoco();
            }
        }
        else
        {
            PerderFoco();
        }
    }

    private void PerderFoco()
    {
        if (isHovering)
        {
            isHovering = false;
            OnShopHover?.Invoke(false); 
        }
    }
}
