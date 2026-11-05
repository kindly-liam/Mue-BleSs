using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Torreta : MonoBehaviour
{
    [Header("Objetivo")]
    [SerializeField] private float rango = 15f;
    [SerializeField] private float alturaObjetivo = 1f;

    [Header("Disparo")]
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private float fireRate = 0.25f;
    [SerializeField] private float bulletSpeed = 35f;

    [Header("Audio")]
    [SerializeField] private AudioClip sonidoDisparo;
    [SerializeField] private float pitchMin = 0.9f;
    [SerializeField] private float pitchMax = 1.1f;

    [Header("Rotación")]
    [SerializeField] private float velocidadRotacion = 360f;
    [SerializeField] private bool dispararSoloCuandoApunta = true;
    [SerializeField] private float anguloMaximoParaDisparar = 5f;

    private EnemyBase objetivoActual;
    private float siguienteDisparo;
    private bool tieneObjetivo;
    private AudioSource fuenteAudio;

    private void Awake()
    {
        fuenteAudio = GetComponent<AudioSource>();

        if (spawnBulletPosition == null)
        {
            Debug.LogError(
                $"La torreta {name} no tiene Spawn Bullet Position asignado.",
                this);
        }

        if (pfBulletProjectile == null)
        {
            Debug.LogError(
                $"La torreta {name} no tiene Pf Bullet Projectile asignado.",
                this);
        }
    }

    private void Update()
    {
        if (!ObjetivoValido())
        {
            BuscarObjetivo();
        }

        if (!tieneObjetivo)
        {
            return;
        }

        Vector3 puntoDeMira = ObtenerPuntoDeMira();
        Vector3 direccion = puntoDeMira - transform.position;

        if (direccion.sqrMagnitude <= 0.001f)
        {
            return;
        }

        Quaternion rotacionObjetivo =
            Quaternion.LookRotation(direccion.normalized, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            rotacionObjetivo,
            velocidadRotacion * Time.deltaTime);

        float diferenciaAngular = Quaternion.Angle(
            transform.rotation,
            rotacionObjetivo);

        bool puedeDisparar =
            !dispararSoloCuandoApunta ||
            diferenciaAngular <= anguloMaximoParaDisparar;

        if (puedeDisparar && Time.time >= siguienteDisparo)
        {
            Disparar(puntoDeMira);
            siguienteDisparo = Time.time + fireRate;
        }
    }

    private bool ObjetivoValido()
    {
        if (objetivoActual == null)
        {
            tieneObjetivo = false;
            return false;
        }

        if (!objetivoActual.isActiveAndEnabled)
        {
            tieneObjetivo = false;
            return false;
        }

        float distancia =
            Vector3.Distance(transform.position, objetivoActual.transform.position);

        if (distancia > rango)
        {
            tieneObjetivo = false;
            objetivoActual = null;
            return false;
        }

        return true;
    }

    private void BuscarObjetivo()
    {
        EnemyBase[] enemigos =
            FindObjectsByType<EnemyBase>(FindObjectsSortMode.None);

        EnemyBase enemigoMasCercano = null;
        float distanciaMasCercana = float.MaxValue;

        foreach (EnemyBase enemigo in enemigos)
        {
            if (enemigo == null || !enemigo.isActiveAndEnabled)
            {
                continue;
            }

            float distancia =
                Vector3.Distance(transform.position, enemigo.transform.position);

            if (distancia <= rango &&
                distancia < distanciaMasCercana)
            {
                distanciaMasCercana = distancia;
                enemigoMasCercano = enemigo;
            }
        }

        objetivoActual = enemigoMasCercano;
        tieneObjetivo = objetivoActual != null;
    }

    private Vector3 ObtenerPuntoDeMira()
    {
        return objetivoActual.transform.position +
               Vector3.up * alturaObjetivo;
    }

    private void Disparar(Vector3 puntoDeMira)
    {
        if (pfBulletProjectile == null ||
            spawnBulletPosition == null)
        {
            return;
        }

        Vector3 direccion =
            (puntoDeMira - spawnBulletPosition.position).normalized;

        Transform bala = Instantiate(
            pfBulletProjectile,
            spawnBulletPosition.position,
            Quaternion.LookRotation(direccion, Vector3.up));

        BulletProjectile proyectil =
            bala.GetComponent<BulletProjectile>();

        if (proyectil != null)
        {
            proyectil.Launch(bulletSpeed);
        }
        else
        {
            Debug.LogWarning(
                $"El prefab {pfBulletProjectile.name} no tiene BulletProjectile.",
                bala);
        }

        ReproducirSonidoDisparo();
    }

    private void ReproducirSonidoDisparo()
    {
        if (fuenteAudio != null && sonidoDisparo != null && fuenteAudio.isActiveAndEnabled)
        {
            fuenteAudio.pitch = Random.Range(pitchMin, pitchMax);
            fuenteAudio.PlayOneShot(sonidoDisparo);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rango);
    }
}