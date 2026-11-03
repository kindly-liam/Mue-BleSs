using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    public void Shoot(Transform spawnBulletPosition, Transform pfBulletProjectile, Vector3 aimDirection)
    {
        
            
            Object.Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up));
            
       
    }
}
