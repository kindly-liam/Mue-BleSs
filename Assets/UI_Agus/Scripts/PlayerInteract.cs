using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [Header("Configuración del Raycast")]
    [SerializeField] private Transform cameraTransform; 
    [SerializeField] private float distanciaInteraccion = 5f; 

    [Header("Referencias UI")]
    [SerializeField] private GameObject shopPanel; 

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
        if (shopPanel != null)
        {
            shopPanel.SetActive(true); 

            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}