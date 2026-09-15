using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Referencias de Instanciación")]
    [SerializeField] private GameObject torretaPrefab; 
    [SerializeField] private Transform spawnPointTorreta; 

    [Header("Referencias UI")]
    [SerializeField] private GameObject shopPanel;

    
    public void ComprarTorreta()
    {
        if (torretaPrefab != null && spawnPointTorreta != null)
        {
            
            Instantiate(torretaPrefab, spawnPointTorreta.position, spawnPointTorreta.rotation);
            Debug.Log("Torreta comprada.");
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