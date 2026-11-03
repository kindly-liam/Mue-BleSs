using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using Unity.Cinemachine;


public class TPSController : MonoBehaviour
{

    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
   // [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private CinemachineCamera aimVirtualCamera;        

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private IWeapon weapon;

    public Vector3 aimDir { get; private set; }

    private void Awake()
    {
        
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();

        weapon = GetComponentInChildren<IWeapon>();

        if (weapon == null)
        {
            Debug.LogError("No se encontró ningún componente que implemente IWeapon.");
        }

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            // debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        

        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            //starterAssetsInputs.cursorInputForLook = false;
             thirdPersonController.SetSensitivity(aimSensitivity);
             thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            aimDir = (worldAimTarget - transform.position).normalized;
           
            transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            //starterAssetsInputs.cursorInputForLook = true;
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }

        if (starterAssetsInputs.shoot && starterAssetsInputs.aim)
        {

            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;

            if (weapon != null)
            {
                weapon.Shoot(spawnBulletPosition, pfBulletProjectile, aimDir);
            }
            else
            {
                Debug.LogWarning("Current weapon does not implement IWeapon interface.");
            }
            
            
            
            
            
            starterAssetsInputs.shoot = false;

        }


    }

   
}
