
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidbody;

    [SerializeField] private int damage = 1;

    
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    public void Launch(float speed)
    {
        if (bulletRigidbody == null)
        {
            Debug.LogError(
                $"El proyectil {name} necesita un componente Rigidbody.");

            return;
        }

        bulletRigidbody.linearVelocity =
            transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
