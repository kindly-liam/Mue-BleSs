using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{

    [SerializeField] private Transform objetivo; // Arrastra aquí al Jugador desde la Jerarquía
    [SerializeField] private int damage;
    public int Damage //Usar esta variable para asignar el daño que causa el enemigo
    {
        get { return damage; }
    }
    [SerializeField] private int health;
    private NavMeshAgent agente;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();

        GameObject tienda = GameObject.FindGameObjectWithTag("Tienda");

        if (tienda != null)
        {
            objetivo = tienda.transform;
        }
    }

    void Update()
    {
        if (objetivo != null)
        {
            agente.destination = objetivo.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;  //Quitar cuando ya consiga el daño que hace la bala del arma del juego (Usar el codigo de abajo)

            //...(collision.gameObject.GetComponent<...>().damage);
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
