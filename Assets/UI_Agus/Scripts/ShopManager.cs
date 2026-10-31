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

    // Eventos para notificar a otros sistemas cuando se compra un arma
    public static event Action OnFusilComprado;
    public static event Action OnSubfusilComprado;

    // --- MÉTODOS PARA LOS BOTONES DE LA UI ---

    public void ComprarTorreta()
    {
        if (torretaPrefab != null && spawnPointTorreta != null)
        {
            if (GoldManager.Instance != null && GoldManager.Instance.TrySpendGold(precioTorreta))
            {
                Instantiate(torretaPrefab, spawnPointTorreta.position, spawnPointTorreta.rotation);
                Debug.Log($"Torreta comprada por {precioTorreta} de oro.");
            }
            else
            {
                Debug.Log("Oro insuficiente para la Torreta.");
            }
        }
    }

    public void ComprarFusil()
    {
        if (GoldManager.Instance != null && GoldManager.Instance.TrySpendGold(precioFusil))
        {
            Debug.Log($"Fusil comprado por {precioFusil} de oro.");
            // Dispara el evento para que el sistema de armas lo escuche
            OnFusilComprado?.Invoke();
        }
        else
        {
            Debug.Log("Oro insuficiente para el Fusil.");
        }
    }

    public void ComprarSubfusil()
    {
        if (GoldManager.Instance != null && GoldManager.Instance.TrySpendGold(precioSubfusil))
        {
            Debug.Log($"Subfusil comprado por {precioSubfusil} de oro.");
            // Dispara el evento para que el sistema de armas lo escuche
            OnSubfusilComprado?.Invoke();
        }
        else
        {
            Debug.Log("Oro insuficiente para el Subfusil.");
        }
    }

    public void CerrarTienda()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}