using System.Collections;
using System.Collections.Generic;   
using UnityEngine;
using UnityEngine.InputSystem;

public class TPSController : MonoBehaviour
{

    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;

    private TPSController tPSController;

    private void Awake()
    {
        tPSController = GetComponent<TPSController>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray= Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
          debugTransform.position = raycastHit.point;
        }
      
    }
}
