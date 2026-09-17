using UnityEngine;

public class Tienda : MonoBehaviour
{
    [SerializeField] private int health;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health--;
            //...(collision.gameObject.GetComponent<...>().Damage);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
