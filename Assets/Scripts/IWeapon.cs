using UnityEngine;

public interface IWeapon 
{
   void Shoot(Vector3 aimDirection);
       int AnimationID { get; }
    GameObject WeaponGameObject { get; }
    float FireRate { get; }
    bool IsAutomatic { get; }
}
