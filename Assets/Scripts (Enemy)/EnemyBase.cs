using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private Transform objetivo; 
    [SerializeField] private int damage = 1;
    [SerializeField] private int health = 5;

    public int Damage
    {
        get { return damage; }
    }

    private NavMeshAgent agente;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();

        
        if (objetivo == null)
        {
            GameObject tienda = GameObject.FindGameObjectWithTag("Tienda");
            if (tienda != null)
            {
                objetivo = tienda.transform;
            }
        }
    }

    void Update()
    {
        
        if (objetivo == null || agente == null) return;

        
        if (agente.isActiveAndEnabled && agente.isOnNavMesh)
        {
            agente.destination = objetivo.position;
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        ProcesarDanio(collision.gameObject);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        ProcesarDanio(other.gameObject);
    }

    private void ProcesarDanio(GameObject objetoColisionado)
    {
        if (objetoColisionado.CompareTag("Bullet"))
        {
            health--;

            
            Destroy(objetoColisionado);

            
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
