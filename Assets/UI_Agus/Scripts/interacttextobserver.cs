using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class InteractTextObserver : MonoBehaviour
{
    private TextMeshProUGUI interactText;

    private void Awake()
    {
        interactText = GetComponent<TextMeshProUGUI>();
        interactText.enabled = false; 
    }

    private void OnEnable()
    {
        InteractionController.OnShopHover += ToggleText;
    }

    private void OnDisable()
    {
        InteractionController.OnShopHover -= ToggleText;
    }

    private void ToggleText(bool show)
    {
        interactText.enabled = show;
    }
}
