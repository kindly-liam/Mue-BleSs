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

    [Header("Configuración de Audio")]
    [SerializeField] private AudioClip sonidoPasoOGrunido; 
    [SerializeField] private AudioClip sonidoDanioRecibido; 
    [SerializeField] private AudioClip sonidoAtaqueTienda;  
    [SerializeField] private AudioClip sonidoMuerte;        

    [Header("Ajustes de Variación SFX")]
    [SerializeField] private float variacionPitchMin = 0.85f;
    [SerializeField] private float variacionPitchMax = 1.15f;

    public int Damage => damage;

    private NavMeshAgent agente;
    private AudioSource fuenteAudio;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();

        
        fuenteAudio = GetComponent<AudioSource>();
        if (fuenteAudio != null && sonidoPasoOGrunido != null)
        {
            fuenteAudio.clip = sonidoPasoOGrunido;
            if (!fuenteAudio.isPlaying)
            {
                fuenteAudio.Play();
            }
        }

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
                //danioARestar = bala.Damage;
            }

            health -= danioARestar;

            
            ReproducirSFXConVariacion(sonidoDanioRecibido);

            Destroy(objetoColisionado);

            if (health <= 0)
            {
                if (GoldManager.Instance != null)
                {
                    GoldManager.Instance.AddGold(recompensaOro);
                }

                
                if (sonidoMuerte != null)
                {
                    AudioSource.PlayClipAtPoint(sonidoMuerte, transform.position, 1f);
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

                
                ReproducirSFXConVariacion(sonidoAtaqueTienda);

                tiempoSiguienteAtaque = Time.time + cadenciaAtaque;
            }
        }
    }

    
    private void ReproducirSFXConVariacion(AudioClip clip)
    {
        if (fuenteAudio != null && clip != null)
        {
            fuenteAudio.pitch = Random.Range(variacionPitchMin, variacionPitchMax);
            fuenteAudio.PlayOneShot(clip);
            fuenteAudio.pitch = 1f;
        }
    }
}