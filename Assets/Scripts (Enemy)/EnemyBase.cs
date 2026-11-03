using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private Transform objetivo;
    [SerializeField] private int damage = 1;
    [SerializeField] private int health = 5;
    [SerializeField] private int recompensaOro = 25;

    [Header("Configuración de Ataque")]
    [SerializeField] private float cadenciaAtaque = 1f; 
    private float tiempoSiguienteAtaque = 0f;

    public int Damage => damage;

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

    private void OnTriggerStay(Collider other)
    {
        
        ProcesarAtaqueContinuo(other.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        ProcesarDanio(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ProcesarDanio(collision.gameObject);
    }

    private void ProcesarDanio(GameObject objetoColisionado)
    {
        if (objetoColisionado.CompareTag("Bullet"))
        {
            
            BulletProjectile bala = objetoColisionado.GetComponent<BulletProjectile>();

            int danioARestar = 1;
            if (bala != null)
            {
                danioARestar = bala.Damage;
            }

            
            health -= danioARestar;

            
            Destroy(objetoColisionado);

            if (health <= 0)
            {
                if (GoldManager.Instance != null)
                {
                    GoldManager.Instance.AddGold(recompensaOro);
                }

                Destroy(gameObject);
            }
        }
    }

    private void ProcesarAtaqueContinuo(GameObject objetoColisionado)
    {
        
        if (objetoColisionado.CompareTag("Tienda") && Time.time >= tiempoSiguienteAtaque)
        {
            ShopHealth saludTienda = objetoColisionado.GetComponentInParent<ShopHealth>();
            if (saludTienda == null)
            {
                saludTienda = objetoColisionado.GetComponent<ShopHealth>();
            }

            if (saludTienda != null)
            {
                saludTienda.TakeDamage(damage);
                tiempoSiguienteAtaque = Time.time + cadenciaAtaque; 
            }
        }
    }
}
