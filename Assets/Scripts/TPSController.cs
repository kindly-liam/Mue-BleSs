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
    private float nextFireTime = 0f; // Controla cuándo se puede volver a disparar

    [Header("Inventario de Armas")]
    [SerializeField] private List<GameObject> allWeaponsInHand;

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
    private void Start()
    {
        IWeapon activeWeapon = null;

        foreach (GameObject weaponObject in allWeaponsInHand)
        {
            if (weaponObject == null || !weaponObject.activeSelf)
            {
                continue;
            }

            activeWeapon = weaponObject.GetComponent<IWeapon>();

            if (activeWeapon != null)
            {
                break;
            }
        }

        if (activeWeapon == null)
        {
            Debug.LogError(
                "No hay ningún arma activa con un componente IWeapon.");

            return;
        }

        EquipWeapon(activeWeapon);
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


            animator.SetInteger("WeaponType", weapon.AnimationID);

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
            if (weapon != null && Time.time >= nextFireTime)
            {
                 Vector3 shootingDirection =
                 (mouseWorldPosition - spawnBulletPosition.position).normalized;

                    this.aimDir = shootingDirection;

                     weapon.Shoot(this.aimDir);
                    nextFireTime = Time.time + weapon.FireRate;
            }

            if (weapon != null && !weapon.IsAutomatic)
            {
                 starterAssetsInputs.shoot = false;
            }
        }
        else if ( starterAssetsInputs.shoot && !starterAssetsInputs.aim)
        {
            starterAssetsInputs.shoot = false;
        }
    }

   
    public void ChangeWeapon(IWeapon newWeapon)
    {
        weapon = newWeapon;

        if (animator.GetBool("IsAiming"))
        {
            animator.SetInteger("WeaponType", weapon.AnimationID);
        }


    }

    public void EquipWeapon(IWeapon newWeapon)
    {
        // Asignamos la nueva estrategia
        weapon = newWeapon;

        // Recorremos todos los GameObjects de armas que tiene el personaje encima
        foreach (GameObject weaponObj in allWeaponsInHand)
        {
            // Si el objeto actual de la lista es el mismo de nuestra nueva estrategia, lo ACTIVAMOS.
            // Si es cualquier otro, lo DESACTIVAMOS.
            if (weaponObj == weapon.WeaponGameObject)
            {
                weaponObj.SetActive(true);
            }
            else
            {
                weaponObj.SetActive(false);
            }
        }

        // Opcional: Si el jugador cambia de arma mientras apunta, actualizamos el Animator inmediatamente
        if (animator.GetInteger("WeaponType") != 0)
        {
            animator.SetInteger("WeaponType", weapon.AnimationID);
        }
    }





}
