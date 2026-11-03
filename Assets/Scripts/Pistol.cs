using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    [SerializeField] private float fireRate = 0.1f; // Tiempo entre disparos en segundos]
    [SerializeField] private float bulletSpeed = 20f; // Velocidad de la bala
    [SerializeField] private Transform pfBulletProjectile; // Prefab de la bala
    [SerializeField] private Transform spawnBulletPosition; // Posición de disparo
    [SerializeField] private AudioSource audioSource; // Fuente de audio para el sonido de disparo

    public void Shoot(Transform spawnBulletPosition, Transform pfBulletProjectile, Vector3 aimDirection)
    {
        
            
            Object.Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up));
            
       
    }
}
