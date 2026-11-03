using System.Xml.Serialization;
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

    private void Start()
    {
        float speed = 20f;
        bulletRigidbody.linearVelocity = transform.forward * speed;
    }



    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
