using UnityEngine;

public interface IWeapon 
{
   void Shoot(Transform spawnBulletPosition, Transform pfBulletProjectile, Vector3 aimDirection);
}
