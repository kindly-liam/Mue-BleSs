using UnityEngine;
using System;

public class ShopManager : MonoBehaviour
{
    [Header("Configuración de Economía")]
    [SerializeField] private int precioTorreta = 100;
    [SerializeField] private int precioFusil = 300;
    [SerializeField] private int precioSubfusil = 200;

    [Header("Referencias de Instanciación (Torreta)")]
    [SerializeField] private GameObject torretaPrefab;
    [SerializeField] private Transform spawnPointTorreta;

    [Header("Referencias UI")]
    [SerializeField] private GameObject shopPanel;

    
    public static event Action OnFusilComprado;
    public static event Action OnSubfusilComprado;

    
    public static event Action OnTiendaAbierta;
    public static event Action OnTiendaCerrada;
    public static event Action OnCompraExitosa;
    public static event Action OnCompraFallida;

    public void ComprarTorreta()
    {
        if (torretaPrefab != null && spawnPointTorreta != null)
        {
            if (GoldManager.Instance != null && GoldManager.Instance.TrySpendGold(precioTorreta))
            {
                Instantiate(torretaPrefab, spawnPointTorreta.position, spawnPointTorreta.rotation);
                Debug.Log($"Torreta comprada por {precioTorreta} de oro.");
                OnCompraExitosa?.Invoke();
            }
            else
            {
                Debug.Log("Oro insuficiente para la Torreta.");
                OnCompraFallida?.Invoke();
            }
        }
    }

    public void ComprarFusil()
    {
        if (GoldManager.Instance != null && GoldManager.Instance.TrySpendGold(precioFusil))
        {
            Debug.Log($"Fusil comprado por {precioFusil} de oro.");
            OnFusilComprado?.Invoke();
            OnCompraExitosa?.Invoke();
        }
        else
        {
            Debug.Log("Oro insuficiente para el Fusil.");
            OnCompraFallida?.Invoke();
        }
    }

    public void ComprarSubfusil()
    {
        if (GoldManager.Instance != null && GoldManager.Instance.TrySpendGold(precioSubfusil))
        {
            Debug.Log($"Subfusil comprado por {precioSubfusil} de oro.");
            OnSubfusilComprado?.Invoke();
            OnCompraExitosa?.Invoke();
        }
        else
        {
            Debug.Log("Oro insuficiente para el Subfusil.");
            OnCompraFallida?.Invoke();
        }
    }

    public void AbrirTienda()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            
            OnTiendaAbierta?.Invoke();
        }
    }

    public void CerrarTienda()
    {
        if (shopPanel != null)
        {
            
            OnTiendaCerrada?.Invoke();

            shopPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}