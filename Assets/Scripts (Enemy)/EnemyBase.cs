using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private Transform objetivo;
    [SerializeField] private int damage = 1;
    [SerializeField] private int health = 5;
    [SerializeField] private int recompensaOro = 25;

    [Header("Configuración de Ataque")]
    [SerializeField] private float cadenciaAtaque = 1f;
    [SerializeField] private float rangoAtaque = 3.5f; // NUEVO: A qué distancia empieza a pegar
    private float tiempoSiguienteAtaque = 0f;
    private ShopHealth saludTienda; // Guardamos la referencia a la tienda para optimizar

    [Header("Configuración de Audio")]
    [SerializeField] private AudioClip sonidoPasoOGrunido;
    [SerializeField] private AudioClip sonidoDanioRecibido;
    [SerializeField] private AudioClip sonidoAtaqueTienda;
    [SerializeField] private AudioClip sonidoMuerte;

    [Header("Ajustes de Variación SFX")]
    [SerializeField] private float variacionPitchMin = 0.85f;
    [SerializeField] private float variacionPitchMax = 1.15f;

    public int Damage => damage;
    private Animator animator;

    private NavMeshAgent agente;
    private AudioSource fuenteAudio;

    public static event Action OnBossDefeated;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agente = GetComponent<NavMeshAgent>();
        fuenteAudio = GetComponent<AudioSource>();

        if (animator == null)
            Debug.LogError($"No se encontró un Animator en {name}.", this);

        if (agente == null)
            Debug.LogError($"No se encontró un NavMeshAgent en {name}.", this);
    }

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        fuenteAudio = GetComponent<AudioSource>();

        if (fuenteAudio != null && sonidoPasoOGrunido != null)
        {
            fuenteAudio.clip = sonidoPasoOGrunido;
            if (!fuenteAudio.isPlaying) fuenteAudio.Play();
        }

        // Buscamos la tienda al aparecer
        if (objetivo == null)
        {
            GameObject tienda = GameObject.FindGameObjectWithTag("Tienda");
            if (tienda != null)
            {
                objetivo = tienda.transform;

                // Intentamos conseguir el script de vida de la tienda de una sola vez
                saludTienda = tienda.GetComponentInParent<ShopHealth>();
                if (saludTienda == null) saludTienda = tienda.GetComponent<ShopHealth>();
            }
        }
    }

    void Update()
    {
        if (objetivo == null || agente == null) return;

        if (agente.isActiveAndEnabled && agente.isOnNavMesh)
        {
            float distancia = Vector3.Distance(transform.position, objetivo.position);

            // Si está más lejos que el rango de ataque, camina hacia el objetivo
            if (distancia > rangoAtaque)
            {
                agente.isStopped = false;
                agente.destination = objetivo.position;
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsAttacking", false); // Apaga el ataque mientras camina
            }
            else
            {
                // Si llegó a la distancia de ataque, frena y ataca sin depender de colliders
                agente.isStopped = true;
                animator.SetBool("IsWalking", false);

                ProcesarAtaquePorDistancia();
            }
        }
    }

    // NUEVA FUNCIÓN: Ataca basado en el tiempo y la distancia, no en TriggerStay
    private void ProcesarAtaquePorDistancia()
    {
        if (Time.time >= tiempoSiguienteAtaque && saludTienda != null)
        {
            animator.SetBool("IsAttacking", true);

            saludTienda.TakeDamage(damage);
            ReproducirSFXConVariacion(sonidoAtaqueTienda);

            tiempoSiguienteAtaque = Time.time + cadenciaAtaque;
        }
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

            if (bala != null) danioARestar = bala.Damage;

            health -= danioARestar;
            ReproducirSFXConVariacion(sonidoDanioRecibido);
            Destroy(objetoColisionado);

            if (health <= 0)
            {
                if (GoldManager.Instance != null) GoldManager.Instance.AddGold(recompensaOro);

                if (sonidoMuerte != null) AudioSource.PlayClipAtPoint(sonidoMuerte, transform.position, 1f);

                if (gameObject.CompareTag("Boss")) OnBossDefeated?.Invoke();

                Destroy(gameObject);
            }
        }
    }

    private void ReproducirSFXConVariacion(AudioClip clip)
    {
        // Se mantiene la prevención de seguridad para que el AudioSource apagado no tire error
        if (fuenteAudio != null && clip != null && fuenteAudio.isActiveAndEnabled)
        {
            fuenteAudio.pitch = UnityEngine.Random.Range(variacionPitchMin, variacionPitchMax);
            float volumenAleatorio = UnityEngine.Random.Range(0.8f, 1.0f);
            fuenteAudio.PlayOneShot(clip, volumenAleatorio);
        }
    }
}