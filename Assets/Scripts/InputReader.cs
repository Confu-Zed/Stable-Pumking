using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Control.IPlayerActions
{
    public Vector2 MovementValue {  get; private set; }
    public event Action JumpEvent;
    public bool IsAttacking { get; private set; } = false;
    Control control;
    private void Awake()
    {
        control = new Control();
        control.Player.SetCallbacks(this);

        control.Enable();
    }
    private void OnDestroy()
    {
        control.Disable();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }
    public void OnBalance(InputAction.CallbackContext context)
    {
        
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            IsAttacking = true;
        else if (context.canceled)
            IsAttacking = false;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        JumpEvent?.Invoke();
    }
}
