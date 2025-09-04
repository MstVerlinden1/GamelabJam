using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BalancingScript : MonoBehaviour
{
    [SerializeField, Tooltip("The text object that displays the balacing value")]
    private TMP_Text text = null;
    
    private float _balancingValue = 0;
    private Vector3 _oldMousePosition = Vector3.zero;
    private Vector3 _mouseDifference = Vector3.zero;
    
    private InputSystem_Actions _inputSystem = new InputSystem_Actions();
    private InputAction _mouseAction;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log(_inputSystem.ToString());
        _mouseAction = _inputSystem.BalancingMicrogame.Mouse;
        _mouseAction.Enable();
    }

    private void OnDisable()
    {
        _mouseAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        _mouseDifference = _mouseAction.ReadValue<Vector3>() - _oldMousePosition;
        _oldMousePosition = _mouseAction.ReadValue<Vector3>();
        Debug.Log(_mouseDifference);
        
        if (text != null) text.SetText(_balancingValue.ToString());
    }
}
