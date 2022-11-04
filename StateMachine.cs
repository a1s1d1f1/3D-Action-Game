using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public IState currentState { get; private set; }

    public bool isTransition { get; private set; }

    public void ChangeState(IState newState)
    {
        if (currentState == newState || isTransition)
            return;

        ChangeStateRoutine(newState);
    }

    void ChangeStateRoutine(IState newState)
    {
        isTransition = true;

        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.Enter();
        }

        isTransition = false;
    }

    void FixedUpdate()
    {
        if (currentState != null && !isTransition)
        {
            currentState.FixedUpdateLogic();
        }
    }

    void Update()
    {
        if (currentState != null && !isTransition)
            currentState.UpdateLogic();
    }
}