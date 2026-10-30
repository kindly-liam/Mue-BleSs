    using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Referencias de Instanciación")]
    [SerializeField] private GameObject torretaPrefab; 
    [SerializeField] private Transform spawnPointTorreta;

    [Header("Configuración de Economía")]
    [SerializeField] private int precioTorreta = 100;

    [Header("Referencias UI")]
    [SerializeField] private GameObject shopPanel;

    
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
                Debug.Log("Compra rechazada: Oro insuficiente.");
            }
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