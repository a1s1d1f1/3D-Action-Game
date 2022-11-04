using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;

    public Vector2 inputValue { get; private set; }
    public bool inputAttackValue { get; private set; }
    public bool isAttackMove { get; private set; }


    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        playerInput.MoveEvent += OnMove;
        playerInput.AttackEvent += OnAttack;
        playerInput.AttackCanceledEvent += OffAttack;
    }

    void Update()
    {
    }

    void OnMove(Vector2 movement)
    {
        inputValue = movement;
    }

    void OnAttack(bool isPressing)
    {
        inputAttackValue = isPressing;
    }

    void OffAttack(bool isReleased)
    {
        inputAttackValue = isReleased;
    }
}