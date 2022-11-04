using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector2> MoveEvent;
    public event Action<bool> AttackEvent;
    public event Action<bool> AttackCanceledEvent;

    private float moveValue;

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("몇번 호출?");
            AttackEvent.Invoke(true);
        }

        if (context.canceled)
        {
            AttackCanceledEvent.Invoke(false);
        }
    }
}