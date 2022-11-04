using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Input inputActions;

    public event Action<Vector2> MoveEvent;
    public event Action<bool> AttackEvent;
    //public event Action<bool> AttackCanceledEvent;

    void Awake()
    {
        inputActions = new Input();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    private void Update()
    {
        inputActions.Player.Attack.started += _ => AttackEvent.Invoke(true);
        inputActions.Player.Attack.canceled += _ => AttackEvent.Invoke(false);
    }

    //public void OnAttack(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        Debug.Log("몇번 호출?");
    //        AttackEvent.Invoke(true);
    //    }

    //    if (context.canceled)
    //    {
    //        AttackCanceledEvent.Invoke(false);
    //    }
    //}
}