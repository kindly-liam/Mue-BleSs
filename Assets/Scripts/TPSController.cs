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
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private CinemachineCamera aimVirtualCamera;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private IWeapon weapon;
    private float nextFireTime = 0f;

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

   
    private void OnEnable()
    {
        ShopManager.OnFusilComprado += EquiparRifleComprado;
        ShopManager.OnSubfusilComprado += EquiparSMGComprada;
    }

    private void OnDisable()
    {
        ShopManager.OnFusilComprado -= EquiparRifleComprado;
        ShopManager.OnSubfusilComprado -= EquiparSMGComprada;
    }

    private void Start()
    {
        
        if (allWeaponsInHand.Count > 0 && allWeaponsInHand[0] != null)
        {
            IWeapon defaultWeapon = allWeaponsInHand[0].GetComponent<IWeapon>();
            if (defaultWeapon != null)
            {
                EquipWeapon(defaultWeapon);
            }
        }
        else
        {
            Debug.LogError("No hay armas configuradas en la lista 'allWeaponsInHand'.");
        }
    }

    void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }

        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
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
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }

        if (starterAssetsInputs.shoot && starterAssetsInputs.aim)
        {
            if (weapon != null && Time.time >= nextFireTime)
            {
                Vector3 shootingDirection = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                this.aimDir = shootingDirection;

                weapon.Shoot(this.aimDir);
                nextFireTime = Time.time + weapon.FireRate;
            }

            if (weapon != null && !weapon.IsAutomatic)
            {
                starterAssetsInputs.shoot = false;
            }
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
        weapon = newWeapon;

        foreach (GameObject weaponObj in allWeaponsInHand)
        {
            if (weaponObj == weapon.WeaponGameObject)
            {
                weaponObj.SetActive(true);
            }
            else
            {
                weaponObj.SetActive(false);
            }
        }

        if (animator.GetInteger("WeaponType") != 0)
        {
            animator.SetInteger("WeaponType", weapon.AnimationID);
        }
    }

    
    private void EquiparSMGComprada()
    {
        
        if (allWeaponsInHand.Count > 1 && allWeaponsInHand[1] != null)
        {
            IWeapon smg = allWeaponsInHand[1].GetComponent<IWeapon>();
            if (smg != null) EquipWeapon(smg);
        }
    }

    private void EquiparRifleComprado()
    {
        
        if (allWeaponsInHand.Count > 2 && allWeaponsInHand[2] != null)
        {
            IWeapon rifle = allWeaponsInHand[2].GetComponent<IWeapon>();
            if (rifle != null) EquipWeapon(rifle);
        }
    }
}
