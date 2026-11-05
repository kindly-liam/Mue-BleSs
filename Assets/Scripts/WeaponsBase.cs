using UnityEngine;

public abstract class WeaponsBase : MonoBehaviour, IWeapon
{
    [SerializeField] protected float fireRate = 0.1f; // Tiempo entre disparos en segundos
    [SerializeField] protected float bulletSpeed = 20f; // Velocidad de la bala
    [SerializeField] protected float damage = 1f; // Daño de la bala
    [SerializeField] protected bool isAutomatic = false;
    [SerializeField] protected Transform pfBulletProjectile; // Prefab de la bala
    [SerializeField] protected Transform spawnBulletPosition; // Posición de disparo
    [SerializeField] private AudioSource audioSource; // Fuente de audio para el sonido de disparo
    [SerializeField] private AudioClip soundDisparo;


    public abstract int AnimationID { get; }
    public GameObject WeaponGameObject => this.gameObject;
    public float FireRate => this.fireRate / FireRateAbilityHandler.MultiplicadorActual;
    public virtual bool IsAutomatic => this.isAutomatic;

    public virtual void Shoot(Vector3 aimDirection)
    {
        if (pfBulletProjectile == null || spawnBulletPosition == null)
        {
            Debug.LogWarning(
                $"El arma {name} no tiene proyectil o punto de disparo asignado.");

            return;
        }

        if (audioSource != null && soundDisparo != null)
        {
            audioSource.PlayOneShot(soundDisparo);
        }

        Transform bullet = Instantiate(
            pfBulletProjectile,
            spawnBulletPosition.position,
            Quaternion.LookRotation(aimDirection, Vector3.up));

        
        BulletProjectile bulletProjectile = bullet.GetComponent<BulletProjectile>();

        if (bulletProjectile != null)
        {
            
            bulletProjectile.Damage = (int)this.damage + DamageAbilityHandler.DanioActualExtra;

            
            bulletProjectile.Launch(bulletSpeed);
        }
        else
        {
            Debug.LogWarning($"El prefab {pfBulletProjectile.name} no tiene BulletProjectile.");
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}